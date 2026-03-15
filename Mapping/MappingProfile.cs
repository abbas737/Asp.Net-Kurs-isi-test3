using AutoMapper;
using Tank_Wiki.Models;
using Tank_Wiki.DTOs.Tank;
using Tank_Wiki.DTOs.General;
using Tank_Wiki.DTOs.TankOfficer;

namespace Tank_Wiki.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // list view
        CreateMap<Tank, TankListDto>();

        // Detail view 
        CreateMap<Tank, TankDetailDto>()
            .ForMember(dest => dest.Officers,
                       opt => opt.MapFrom(src => src.Officers))
            .ForMember(dest => dest.Generals,
                       opt => opt.MapFrom(src => src.Generals));

        // Officer ve General

        CreateMap<TankOfficer, TankOfficerDto>();

        CreateMap<General, GeneralDto>();

        // Create ve Update

        CreateMap<CreateTankDto, Tank>();

        CreateMap<UpdateTankDto, Tank>()
            
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}