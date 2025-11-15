using AutoMapper;
using HIAST.Transportation.Application.DTOs.Trip;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class TripProfile : Profile
{
    public TripProfile()
    {
        // Maps for reading data, including nested details.
        CreateMap<Trip, TripDto>()
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line.Name))
            .ForMember(dest => dest.BusLicensePlate, opt => opt.MapFrom(src => src.Bus.LicensePlate))
            .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.Driver.Name));
            
        CreateMap<Trip, TripListDto>()
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line.Name))
            .ForMember(dest => dest.BusLicensePlate, opt => opt.MapFrom(src => src.Bus.LicensePlate))
            .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.Driver.Name));

        // Maps for writing data.
        CreateMap<CreateTripDto, Trip>();
        CreateMap<UpdateTripDto, Trip>();
    }
}