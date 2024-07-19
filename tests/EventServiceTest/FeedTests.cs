using System.Net.Http.Json;
using MongoDB.Driver;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Domain.Extensions;
using Xunit;

namespace Sportalytics.Feed.Test.Integration;

public class FeedTests(IntegrationTestAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task ShouldCreateSportEvent()
    {
        // Arrange
        var createSportEventDto = new CreateSportEventDto
        {
            Name = "Test Sport Event",
            Location = "Test Location",
            Date = DateTime.UtcNow
        };

        // Act
        var response = await Client.PostAsJsonAsync("api/sport-events", createSportEventDto);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        var sportEventResponse = await response.Content.ReadFromJsonAsync<CreateSportEventDto>().EnsureExists();

        Assert.Equal(createSportEventDto.Name, sportEventResponse.Name);
        Assert.Equal(createSportEventDto.Location, sportEventResponse.Location);
        Assert.Equal(createSportEventDto.Date.ToString("d"), sportEventResponse.Date.ToString("d"));

        var data = await SportEventRepository.Query(e => e.Name == createSportEventDto.Name).ToListAsync();
        var sportEvent = data.FirstOrDefault().EnsureExists();
        Assert.NotNull(data);

        Assert.Equal(sportEventResponse.Name, sportEvent.Name);
        Assert.Equal(sportEventResponse.Location, sportEvent.Location);
        Assert.Equal(sportEventResponse.Date.ToString("d"), sportEvent.Date.ToString("d"));
    }
}