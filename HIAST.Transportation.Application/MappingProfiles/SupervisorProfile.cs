using AutoMapper;
using HIAST.Transportation.Application.DTOs.Supervisor;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class SupervisorProfile : Profile
{
    public SupervisorProfile()
    {
        // Mapping from Line entity to the SupervisorLineDto.
        // Note: The handler performs a manual select for efficiency and to construct EmployeeName.
        CreateMap<Line, SupervisorLineDto>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.SupervisorId))
            .ForMember(dest => dest.EmployeeNumber, opt => opt.MapFrom(src => src.Supervisor.EmployeeNumber))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Supervisor.FirstName + " " + src.Supervisor.LastName))
            .ForMember(dest => dest.LineId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Name));
    }
}