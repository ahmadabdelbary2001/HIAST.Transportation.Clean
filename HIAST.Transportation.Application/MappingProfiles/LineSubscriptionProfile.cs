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
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeUserId))
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line.Name));
            
        CreateMap<LineSubscription, LineSubscriptionListDto>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeUserId))
            .ForMember(dest => dest.LineId, opt => opt.MapFrom(src => src.LineId))
            .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line.Name));

        // Maps for writing data
        CreateMap<CreateLineSubscriptionDto, LineSubscription>()
            .ForMember(dest => dest.EmployeeUserId, opt => opt.MapFrom(src => src.EmployeeId));
        CreateMap<UpdateLineSubscriptionDto, LineSubscription>()
            .ForMember(dest => dest.EmployeeUserId, opt => opt.MapFrom(src => src.EmployeeId));
    }
}