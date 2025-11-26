using AutoMapper;
using HIAST.Transportation.Application.DTOs.LineSubscription;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class LineSubscriptionProfile : Profile
{
    public LineSubscriptionProfile()
    {
        // Maps for reading data
        CreateMap<LineSubscription, LineSubscriptionDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line.Name));
            
        CreateMap<LineSubscription, LineSubscriptionListDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line.Name));

        // Maps for writing data
        CreateMap<LineSubscription, CreateLineSubscriptionDto>().ReverseMap();
        CreateMap<LineSubscription, UpdateLineSubscriptionDto>().ReverseMap();
    }
}