using Refit;
using SheepBot.iRacing.Client.Models.Responses;

namespace SheepBot.iRacing.Client.Interfaces;

// ReSharper disable once InconsistentNaming
public interface iRacingApi : IDisposable
{
    [Get("/data/track/get")]
    Task<ApiResponse<LinkResponse>> GetAllTracks();

    [Get("/data/car/get")]
    Task<ApiResponse<LinkResponse>> GetAllCars();
    
    [Get("/data/carclass/get")]
    Task<ApiResponse<LinkResponse>> GetAllCarClasses();
}