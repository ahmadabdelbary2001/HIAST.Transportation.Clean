using AutoMapper;
using HIAST.Transportation.Application.DTOs.Driver;
using HIAST.Transportation.Application.DTOs.Driver.Validators;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        // Maps for reading data
        CreateMap<DriverWithLineDetails, DriverDto>()
            // Map properties from the root Driver object
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Driver.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Driver.Name))
            .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.Driver.LicenseNumber))
            .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.Driver.ContactInfo))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Driver.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Driver.UpdatedAt))
            // Map properties from the joined Line and Bus objects (they can be null)
            .ForMember(dest => dest.LineId, opt => opt.MapFrom(src => src.Line != null ? src.Line.Id : (int?)null))
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line != null ? src.Line.Name : null))
            .ForMember(dest => dest.BusId, opt => opt.MapFrom(src => src.Line != null ? src.Line.BusId : (int?)null))
            .ForMember(dest => dest.BusLicensePlate, opt => opt.MapFrom(src => src.Bus != null ? src.Bus.LicensePlate : null));
        
        CreateMap<Driver, DriverListDto>();
        
        // Maps for writing data
        CreateMap<CreateDriverDto, Driver>();
        CreateMap<UpdateDriverDto, Driver>();
    }
}