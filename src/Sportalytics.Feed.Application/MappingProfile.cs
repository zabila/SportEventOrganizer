using AutoMapper;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Domain.Entities;

namespace Sportalytics.Feed.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateSpotEventDto, SportEvent>();
        CreateMap<SportEvent, ResponseSpotEventDto>();
        CreateMap<List<SportEvent>, List<ResponseSpotEventDto>>();
    }
}