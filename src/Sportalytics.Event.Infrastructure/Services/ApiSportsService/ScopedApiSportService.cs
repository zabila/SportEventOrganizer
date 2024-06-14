using System.Net.Http.Headers;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sportalytics.Event.Application.Commands;
using Sportalytics.Event.Application.DTOs;
using Sportalytics.Event.Domain.Entities;
using Sportalytics.Event.Domain.Entities.ApiSports;
using Sportalytics.Event.Domain.Exceptions;
using Sportalytics.Event.Domain.Extensions;
using Sportalytics.Event.Infrastructure.Interfaces;

namespace Sportalytics.Event.Infrastructure.Services.ApiSportsService;

public class ScopedApiSportService : IScopedApiSportService
{
    private readonly ISender _sender;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHttpClientFactory _clientFactory;
    private readonly ApiSportsServiceSettings _apiSportsServiceSettings;

    private readonly TimeSpan _timeToNextRun;
    private readonly TimeSpan _timeForRetry;

    public ScopedApiSportService(IHttpClientFactory clientFactory, ISender sender, IOptions<ApiSportsServiceSettings> serviceSettings, IServiceScopeFactory serviceScopeFactory)
    {
        _clientFactory = clientFactory;
        _sender = sender;
        _serviceScopeFactory = serviceScopeFactory;
        _apiSportsServiceSettings = serviceSettings.Value;
        _timeToNextRun = _apiSportsServiceSettings.TimeToNextRun;
        _timeForRetry = _apiSportsServiceSettings.TimeForRetry;
    }

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        var client = CreateClient();
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = await SendRequestAsync(client, stoppingToken);
                if (result is null)
                {
                    Console.WriteLine("Failed to get data from the API. Retrying in 10 seconds.");
                    await Task.Delay(_timeForRetry, stoppingToken);
                    continue;
                }

                var json = result.RootElement;

                var error = json.GetProperty("errors").ToString();
                if (error != "[]" || string.IsNullOrEmpty(error))
                {
                    var errorModel = JsonSerializer.Deserialize<ErrorModel>(error);
                    if (errorModel?.Token is not null)
                        throw new ApiSportTokenNotFoundException();
                }

                var response = json.GetProperty("response").ToString().EnsureExists();
                var apiResponse = JsonSerializer.Deserialize<List<Response>>(response).EnsureExists();

                var responseDto = new ProcessApiSportsResponseDto(apiResponse);
                var command = new ProcessSportEventsCommand(responseDto, stoppingToken);
                await _sender.Send(command, stoppingToken);

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to get data from the API: {ex.Message}. Retrying in 10 seconds.");
                await Task.Delay(_timeForRetry, stoppingToken);
                continue;
            }

            await Task.Delay(_timeToNextRun, stoppingToken);
        }
    }

    private async Task<JsonDocument?> SendRequestAsync(HttpClient client, CancellationToken stoppingToken)
    {
        var baseUrl = _apiSportsServiceSettings.BaseUrl.EnsureExists();
        var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
        var requestUri = $"{baseUrl}fixtures?date={today}&status=NS";

        var response = await client.GetAsync(requestUri, stoppingToken);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Request to {requestUri} failed with status code {response.StatusCode}.");

        var responseBody = await response.Content.ReadAsStringAsync(stoppingToken);
        var jsonDoc = JsonDocument.Parse(responseBody);
        return jsonDoc;
    }

    private HttpClient CreateClient()
    {
        var client = _clientFactory.CreateClient("SportsDataIO");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var apiKey = _apiSportsServiceSettings.ApiKey.EnsureExists();
        client.DefaultRequestHeaders.Add("x-apisports-key", apiKey);

        return client;
    }
}