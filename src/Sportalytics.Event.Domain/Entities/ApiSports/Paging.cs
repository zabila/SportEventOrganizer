﻿using System.Text.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class Paging
{
    [JsonPropertyName("current")]
    public int Current { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}