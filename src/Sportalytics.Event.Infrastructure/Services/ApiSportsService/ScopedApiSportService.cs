using System.Net.Http.Headers;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sportalytics.Event.Application.Commands;
using Sportalytics.Event.Application.DTOs;
using Sportalytics.Event.Domain.Entities;
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
            var result = await SendRequestAsync(client, stoppingToken);
            if (result is null)
            {
                Console.WriteLine("Failed to get data from the API. Retrying in 10 seconds.");
                await Task.Delay(_timeForRetry, stoppingToken);
                continue;
            }

            var json = result.RootElement;
            var jsonString = JsonSerializer.Serialize(json);
            var apiResponse = JsonSerializer.Deserialize<ApiSportResponse>(jsonString);
            if (apiResponse is null)
            {
                Console.WriteLine("Failed to deserialize the API response. Retrying in 10 seconds.");
                await Task.Delay(_timeForRetry, stoppingToken);
                continue;
            }

            var responseDto = new ProcessApiSportsResponseDto(apiResponse.Response);
            var command = new ProcessSportEventsCommand(responseDto, stoppingToken);
            await _sender.Send(command, stoppingToken);

            await Task.Delay(_timeToNextRun, stoppingToken);
        }
    }

    private async Task<JsonDocument?> SendRequestAsync(HttpClient client, CancellationToken stoppingToken)
    {
        var baseUrl = _apiSportsServiceSettings.BaseUrl;
        ArgumentException.ThrowIfNullOrEmpty(baseUrl);

        var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
        var requestUri = $"{baseUrl}fixtures?date={today}&status=NS";

        var response = await client.GetAsync(requestUri, stoppingToken);
        if (!response.IsSuccessStatusCode)
            return null;

        var responseBody = await response.Content.ReadAsStringAsync(stoppingToken);
        var jsonDoc = JsonDocument.Parse(responseBody);
        return jsonDoc;
    }

    private HttpClient CreateClient()
    {
        var client = _clientFactory.CreateClient("SportsDataIO");

        var apiKey = _apiSportsServiceSettings.ApiKey;
        ArgumentException.ThrowIfNullOrEmpty(apiKey);

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("x-apisports-key", apiKey);

        return client;
    }
}