using SheepBot.iRacing.Client.Models;

namespace SheepBot.iRacing.Client.Interfaces;

// ReSharper disable once InconsistentNaming
public interface IiRacingService
{
    Task<IEnumerable<iRacingTrack>> GetAllTracks();
    Task<IEnumerable<iRacingCar>> GetAllCars();
    Task<IEnumerable<iRacingCarClass>> GetAllCarClasses();
}