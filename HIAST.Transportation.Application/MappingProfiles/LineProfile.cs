using AutoMapper;
using HIAST.Transportation.Application.DTOs.Line;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class LineProfile : Profile
{
    public LineProfile()
    {
        // Maps for reading data, including the nested Supervisor name.
        CreateMap<Line, LineDto>()
            .ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.Name));
            
        CreateMap<Line, LineListDto>()
            .ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.Name));

        // Maps for writing data.
        CreateMap<CreateLineDto, Line>();
        CreateMap<UpdateLineDto, Line>();
    }
}