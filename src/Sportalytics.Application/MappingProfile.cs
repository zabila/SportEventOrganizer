using AutoMapper;
using Sportalytics.Application.DTOs;
using Sportalytics.Domain.Entities;

namespace Sportalytics.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateSpotEventDto, SportEvent>();
        CreateMap<SportEvent, ResponseSpotEventDto>();
    }
}