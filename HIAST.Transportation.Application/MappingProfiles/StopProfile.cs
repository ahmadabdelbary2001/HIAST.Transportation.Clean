using AutoMapper;
using HIAST.Transportation.Application.DTOs.Stop;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class StopProfile : Profile
{
    public StopProfile()
    {
        // Maps for reading data
        CreateMap<Stop, StopDto>().ReverseMap();
        CreateMap<Stop, StopListDto>().ReverseMap();

        // Maps for writing data
        CreateMap<CreateStopDto, Stop>();
        CreateMap<UpdateStopDto, Stop>();
    }
}