using AutoMapper;
using HIAST.Transportation.Application.DTOs.LineStop;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class LineStopProfile : Profile
{
    public LineStopProfile()
    {
        // Maps for reading data, including nested Line and Stop names.
        CreateMap<LineStop, LineStopDto>()
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line.Name))
            .ForMember(dest => dest.StopName, opt => opt.MapFrom(src => src.Stop.Name));
            
        CreateMap<LineStop, LineStopListDto>()
            .ForMember(dest => dest.StopName, opt => opt.MapFrom(src => src.Stop.Name));

        // Maps for writing data.
        CreateMap<CreateLineStopDto, LineStop>();
        CreateMap<UpdateLineStopDto, LineStop>();
    }
}