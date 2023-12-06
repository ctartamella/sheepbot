using AutoMapper;
using SheepBot.iRacing.Client.Models;

namespace SheepBot.Application.Application.Car;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<iRacingCar, Domain.Entities.Car>()
            .ForMember(c => c.Name, x => x.MapFrom(c => c.CarName))
            .ForMember(c => c.IsFree, x => x.MapFrom(c => c.FreeWithSubscription))
            .ForMember(c => c.IsLegacy,
                x => x.MapFrom(c => c.CarName.Contains("LEGACY", StringComparison.InvariantCulture)));

        CreateMap<iRacingCarClass, Domain.Entities.Class>()
            .ForMember(c => c.Name, x => x.MapFrom(c => c.Name))
            .ForMember(c => c.ClassId, x => x.MapFrom(c => c.CarClassId));

    }
}