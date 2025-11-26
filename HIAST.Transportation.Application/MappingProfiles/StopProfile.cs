using AutoMapper;
using HIAST.Transportation.Application.DTOs.Stop;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class StopProfile : Profile
{
    public StopProfile()
    {
        // Maps for reading data
        CreateMap<Stop, StopDto>()
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line != null ? src.Line.Name : string.Empty));
            
        CreateMap<Stop, StopListDto>()
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line != null ? src.Line.Name : string.Empty));

        // Maps for writing data
        CreateMap<CreateStopDto, Stop>();
        CreateMap<UpdateStopDto, Stop>();
    }
}