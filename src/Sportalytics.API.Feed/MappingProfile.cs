using AutoMapper;
using Sportalytics.Application.Feed.Queries;
using Sportalytics.Domain.DTOs;
using Sportalytics.Domain.Entities;

namespace Sportalytics.API.Feed;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateSpotEventDto, SportEvent>();
        CreateMap<SportEvent, ResponseSpotEventDto>();
    }
}