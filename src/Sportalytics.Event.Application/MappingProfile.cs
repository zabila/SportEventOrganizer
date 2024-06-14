using AutoMapper;
using Sportalytics.Event.Domain.Entities;
using Sportalytics.Event.Domain.Entities.ApiSports;

namespace Sportalytics.Event.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Response, SportEvent>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Teams!.Home!.Name + " vs " + src.Teams!.Away!.Name))
            .ForMember(dest => dest.Location,
                opt => opt.MapFrom(src => src.Fixture!.Venue!.City))
            .ForMember(dest => dest.Date,
                opt => opt.MapFrom(src => src.Fixture!.Date!.Date));
    }
}