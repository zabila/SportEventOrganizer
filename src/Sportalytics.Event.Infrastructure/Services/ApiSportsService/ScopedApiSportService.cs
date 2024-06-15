using System.Net.Http.Headers;
using System.Text.Json;
using Flurl.Http;
using Flurl.Http.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sportalytics.Event.Application.Commands;
using Sportalytics.Event.Application.DTOs;
using Sportalytics.Event.Domain.Entities.ApiSports;
using Sportalytics.Event.Domain.Exceptions;
using Sportalytics.Event.Domain.Extensions;
using Sportalytics.Event.Infrastructure.Interfaces;

namespace Sportalytics.Event.Infrastructure.Services.ApiSportsService;

public class ScopedApiSportService(IFlurlClientCache flurlClientCache, ISender sender, IOptions<ApiSportsServiceSettings> serviceSettings)
    : IScopedApiSportService
{
    private readonly ApiSportsServiceSettings _apiSportsServiceSettings = serviceSettings.Value;

    public async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        var result = await SendRequestAsync(cancellationToken);

        HandleErrors(result.RootElement);
        await ParseAndSendCommand(result.RootElement, cancellationToken);
    }

    private async Task<JsonDocument> SendRequestAsync(CancellationToken cancellationToken)
    {
        var baseUrl = _apiSportsServiceSettings.BaseUrl.EnsureExists();
        var apiKey = _apiSportsServiceSettings.ApiKey.EnsureExists();
        var today = DateTime.UtcNow.ToString("yyyy-MM-dd");

        var response = await flurlClientCache
            .Get(nameof(ScopedApiSportService))
            .Request("fixtures")
            .SetQueryParams(new
            {
                date = today,
                status = "NS"
            })
            .WithHeader("Accept", "application/json")
            .WithHeader("x-apisports-key", apiKey)
            .GetJsonAsync<JsonDocument>(cancellationToken: cancellationToken)
            .EnsureExists();

        return response;
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
        await sender.Send(command, stoppingToken);
    }
}