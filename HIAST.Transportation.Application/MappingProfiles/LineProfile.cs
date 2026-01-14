using AutoMapper;
using HIAST.Transportation.Application.DTOs.Line;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class LineProfile : Profile
{
    public LineProfile()
    {
        // Maps for reading data
        CreateMap<Line, LineDto>()
            .ForMember(dest => dest.Subscriptions, opt => opt.MapFrom(src => src.LineSubscriptions));

        CreateMap<Line, LineListDto>();
        
        // Maps for writing data.
        CreateMap<CreateLineDto, Line>();
        CreateMap<UpdateLineDto, Line>();
    }
}