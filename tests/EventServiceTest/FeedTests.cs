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
            Name = "Test Create Sport Event",
            Location = "Test Location",
            Date = DateTime.UtcNow
        };

        // Act
        var response = await Client.PostAsJsonAsync("api/sport-events", createSportEventDto);

        // Assert
        response.EnsureSuccessStatusCode();
        var sportEventResponse = await response.Content.ReadFromJsonAsync<CreateSportEventDto>();
        Assert.NotNull(sportEventResponse);

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

    [Fact]
    public async Task ShouldGetSportEvent()
    {
        // Arrange
        var createSportEventDto = new CreateSportEventDto
        {
            Name = "Test Get Sport Event",
            Location = "Test Location",
            Date = DateTime.UtcNow
        };
        // Act
        var createdSportEventResponse = await Client.PostAsJsonAsync("api/sport-events", createSportEventDto);
        createdSportEventResponse.EnsureSuccessStatusCode();
        var createdSportEvent = await createdSportEventResponse.Content.ReadFromJsonAsync<CreateSportEventDto>();
        Assert.NotNull(createdSportEvent);

        var data = await SportEventRepository.Query(e => e.Name == createSportEventDto.Name).ToListAsync();
        var sportEvent = data.FirstOrDefault().EnsureExists();
        Assert.NotNull(data);

        var getSportEventResponse = await Client.GetAsync($"api/sport-events/{sportEvent.Id}");
        getSportEventResponse.EnsureSuccessStatusCode();
        var getSportEvent = await getSportEventResponse.Content.ReadFromJsonAsync<CreateSportEventDto>();
        Assert.NotNull(getSportEvent);

        // Assert
        Assert.Equal(createSportEventDto.Name, createdSportEvent.Name);
        Assert.Equal(createSportEventDto.Location, createdSportEvent.Location);
        Assert.Equal(createSportEventDto.Date.ToString("d"), createdSportEvent.Date.ToString("d"));

        Assert.Equal(getSportEvent.Name, sportEvent.Name);
        Assert.Equal(getSportEvent.Location, sportEvent.Location);
        Assert.Equal(getSportEvent.Date.ToString("d"), sportEvent.Date.ToString("d"));
    }

    [Fact]
    public async Task ShouldUpdateSportEvent()
    {
        // Arrange
        var createSportEventDto = new CreateSportEventDto
        {
            Name = "Test Update Sport Event",
            Location = "Test Location",
            Date = DateTime.UtcNow
        };

        var updateSportEventDto = new UpdateSpotEventDto
        {
            Name = "Updated Test Sport Event",
            Location = "Updated Test Location",
            Date = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var createdSportEventResponse = await Client.PostAsJsonAsync("api/sport-events", createSportEventDto);
        createdSportEventResponse.EnsureSuccessStatusCode();
        var createdSportEvent = await createdSportEventResponse.Content.ReadFromJsonAsync<CreateSportEventDto>();
        Assert.NotNull(createdSportEvent);

        var data = await SportEventRepository.Query(e => e.Name == createSportEventDto.Name).ToListAsync();
        var sportEvent = data.FirstOrDefault().EnsureExists();
        Assert.NotNull(data);

        var updateSportEventResponse = await Client.PutAsJsonAsync($"api/sport-events/{sportEvent.Id}", updateSportEventDto);
        updateSportEventResponse.EnsureSuccessStatusCode();

        var updatedData = await SportEventRepository.Query(e => e.Name == updateSportEventDto.Name).ToListAsync();
        var updatedSportEvent = updatedData.FirstOrDefault().EnsureExists();
        Assert.NotNull(updatedData);

        // Assert
        Assert.Equal(updateSportEventDto.Name, updatedSportEvent.Name);
        Assert.Equal(updateSportEventDto.Location, updatedSportEvent.Location);
        Assert.Equal(updateSportEventDto.Date.ToString("d"), updatedSportEvent.Date.ToString("d"));
    }

    [Fact]
    public async Task ShouldDeleteSportEvent()
    {
        // Arrange
        var createSportEventDto = new CreateSportEventDto
        {
            Name = "Test Delete Sport Event",
            Location = "Test Location",
            Date = DateTime.UtcNow
        };

        // Act
        var createdSportEventResponse = await Client.PostAsJsonAsync("api/sport-events", createSportEventDto);
        createdSportEventResponse.EnsureSuccessStatusCode();
        var createdSportEvent = await createdSportEventResponse.Content.ReadFromJsonAsync<CreateSportEventDto>();
        Assert.NotNull(createdSportEvent);

        var data = await SportEventRepository.Query(e => e.Name == createSportEventDto.Name).ToListAsync();
        var sportEvent = data.FirstOrDefault().EnsureExists();
        Assert.NotNull(data);

        var deleteSportEventResponse = await Client.DeleteAsync($"api/sport-events/{sportEvent.Id}");
        deleteSportEventResponse.EnsureSuccessStatusCode();

        var deletedData = await SportEventRepository.Query(e => e.Name == createSportEventDto.Name).ToListAsync();
        Assert.Empty(deletedData);
    }
}