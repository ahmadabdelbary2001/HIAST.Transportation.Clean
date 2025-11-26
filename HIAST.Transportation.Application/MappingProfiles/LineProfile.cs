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
            // Add this line to map the Supervisor's name
            .ForMember(
                dest => dest.SupervisorName,
                opt => opt.MapFrom(src => $"{src.Supervisor.FirstName} {src.Supervisor.LastName}")
            )
            .ForMember(dest => dest.Subscriptions, opt => opt.MapFrom(src => src.LineSubscriptions));

        CreateMap<Line, LineListDto>()
            // And add this line for the list DTO as well
            .ForMember(
                dest => dest.SupervisorName,
                opt => opt.MapFrom(src => $"{src.Supervisor.FirstName} {src.Supervisor.LastName}")
            );
        
        // Maps for writing data.
        CreateMap<CreateLineDto, Line>();
        CreateMap<UpdateLineDto, Line>();
    }
}