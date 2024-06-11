using AutoMapper;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Domain.Entities;

namespace Sportalytics.Feed.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateSportEventDto, SportEvent>();
        CreateMap<SportEvent, ResponseSportEventDto>();
        CreateMap<UpdateSpotEventDto, SportEvent>();
    }
}