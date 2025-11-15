using AutoMapper;
using HIAST.Transportation.Application.DTOs.Employee;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Employee, EmployeeListDto>().ReverseMap();
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();
    }
}