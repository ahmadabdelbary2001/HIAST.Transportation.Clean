using AutoMapper;
using HIAST.Transportation.Application.DTOs.Line;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class LineProfile : Profile
{
    public LineProfile()
    {
        // Maps for reading data, including the nested Supervisor name.
        CreateMap<Line, LineDto>();
        CreateMap<Line, LineListDto>();
        
        // Maps for writing data.
        CreateMap<CreateLineDto, Line>();
        CreateMap<UpdateLineDto, Line>();
    }
}