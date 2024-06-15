using System.Net.Http.Headers;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sportalytics.Event.Application.Commands;
using Sportalytics.Event.Application.DTOs;
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
        var result = await SendRequestAsync(client, stoppingToken);
        var json = result.EnsureExists().RootElement;

        HandleErrors(json);
        await ParseAndSendCommand(json, stoppingToken);
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

    private static void HandleErrors(JsonElement json)
    {
        var error = json.GetProperty("errors").ToString();
        if (error == "[]")
            return;

        if (error.Contains("token"))
            throw new ApiSportTokenInvalidException();
    }

    private async Task ParseAndSendCommand(JsonElement json, CancellationToken stoppingToken)
    {
        var response = json.GetProperty("response").ToString().EnsureExists();
        var apiResponse = JsonConvert.DeserializeObject<List<Response>>(response).EnsureExists();

        var responseDto = new ProcessApiSportsResponseDto(apiResponse);
        var command = new ProcessSportEventsCommand(responseDto, stoppingToken);
        await _sender.Send(command, stoppingToken);
    }
}