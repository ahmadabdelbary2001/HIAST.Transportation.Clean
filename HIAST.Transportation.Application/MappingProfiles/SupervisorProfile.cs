using AutoMapper;
using HIAST.Transportation.Application.DTOs.Supervisor;
using HIAST.Transportation.Domain.Entities;

namespace HIAST.Transportation.Application.MappingProfiles;

public class SupervisorProfile : Profile
{
    public SupervisorProfile()
    {
        CreateMap<Supervisor, SupervisorDto>().ReverseMap();
        CreateMap<Supervisor, SupervisorListDto>().ReverseMap();
        CreateMap<CreateSupervisorDto, Supervisor>();
        CreateMap<UpdateSupervisorDto, Supervisor>();
    }
}