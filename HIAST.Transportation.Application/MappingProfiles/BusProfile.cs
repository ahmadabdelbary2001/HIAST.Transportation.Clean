using AutoMapper;
using HIAST.Transportation.Application.DTOs.Bus;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class BusProfile : Profile
{
    public BusProfile()
    {
        // Maps for reading data
        CreateMap<Bus, BusDto>().ReverseMap();
        CreateMap<Bus, BusListDto>().ReverseMap();

        // Maps for writing data
        CreateMap<CreateBusDto, Bus>();
        CreateMap<UpdateBusDto, Bus>();
    }
}