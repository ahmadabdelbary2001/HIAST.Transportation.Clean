using AutoMapper;
using HIAST.Transportation.Application.DTOs.Employee;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        // Maps for reading data
        CreateMap<Employee, EmployeeDto>()
            .ForMember(
                dest => dest.SubscribedLineName,
                opt => opt.MapFrom(src => src.Subscription != null ? src.Subscription.Line.Name : null)
            )
            .ForMember(
                dest => dest.SubscribedLineId,
                opt => opt.MapFrom(src => src.Subscription != null ? (int?)src.Subscription.LineId : null)
            );
        CreateMap<Employee, EmployeeListDto>().ReverseMap();
        
        // Maps for writing data
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();
    }
}