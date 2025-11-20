using AutoMapper;
using HIAST.Transportation.Application.DTOs.LineSubscription;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class LineSubscriptionProfile : Profile
{
    public LineSubscriptionProfile()
    {
        // Maps for reading data
        CreateMap<LineSubscription, LineSubscriptionDto>().ReverseMap();
        CreateMap<LineSubscription, LineSubscriptionListDto>().ReverseMap();

        // Maps for writing data
        CreateMap<LineSubscription, CreateLineSubscriptionDto>().ReverseMap();
        CreateMap<LineSubscription, UpdateLineSubscriptionDto>().ReverseMap();
    }
}