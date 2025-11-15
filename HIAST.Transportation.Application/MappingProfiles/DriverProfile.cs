using AutoMapper;
using HIAST.Transportation.Application.DTOs.Driver;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        CreateMap<Driver, DriverDto>().ReverseMap();
        CreateMap<Driver, DriverDto>().ReverseMap();
        CreateMap<CreateDriverDto, Driver>();
        CreateMap<UpdateDriverDto, Driver>();
    }
}