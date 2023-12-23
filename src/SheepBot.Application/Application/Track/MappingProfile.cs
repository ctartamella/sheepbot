using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using NetTopologySuite.Geometries;
using SheepBot.Domain.Entities;
using SheepBot.Domain.Enums;
using SheepBot.iRacing.Client.Models;

namespace SheepBot.Application.Application.Track;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<iRacingTrackCategory, TrackType>().ConvertUsingEnumMapping(x =>
        {
            x.MapValue(iRacingTrackCategory.Oval, TrackType.Oval);
            x.MapValue(iRacingTrackCategory.Road, TrackType.Road);
            x.MapValue(iRacingTrackCategory.DirtOval, TrackType.DirtOval);
            x.MapValue(iRacingTrackCategory.Rallycross, TrackType.DirtRoad);
        });
        
        CreateMap<iRacing.Client.Models.iRacingTrack, Domain.Entities.Track>()
            .ForMember(t => t.TrackId, x => x.MapFrom(t => t.TrackId))
            .ForMember(t => t.Name, x => x.MapFrom(t => t.TrackName))
            .ForMember(t => t.IsFree, x => x.MapFrom(t => t.FreeWithSubscription))
            .ForMember(t => t.IsLegacy, x => x.MapFrom(t => t.TrackName.Contains("LEGACY", StringComparison.InvariantCulture)))
            .ForMember(t => t.GeoLocation, x => x.MapFrom(t => new Point(t.Longitude, t.Latitude)))
            .ForMember(t => t.Price, x => x.MapFrom(t => t.PriceDisplay))
            .ForMember(t => t.IsPurchasable, x => x.MapFrom(t => t.Purchasable))
            .ForMember(t => t.IsRetired, x => x.MapFrom(t => t.Retired));

        CreateMap<iRacing.Client.Models.iRacingTrack, TrackConfig>()
            .ForMember(t => t.Name, x => x.MapFrom(t => t.TrackName))
            .ForMember(t => t.Corners, x => x.MapFrom(t => t.CornersPerLap))
            .ForMember(t => t.Length, x => x.MapFrom(t => t.TrackConfigLength))
            .ForMember(t => t.PitSpeedLimit, x => x.MapFrom(t => t.PitRoadSpeedLimit))
            .ForMember(t => t.TrackType, x => x.MapFrom(t => t.Category));
    }
}