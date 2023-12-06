using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;
using SheepBot.iRacing.Client.Interfaces;
using SheepBot.iRacing.Client.Models;
using SheepBot.iRacing.Client.Models.Responses;

namespace SheepBot.iRacing.Client.Services;

// ReSharper disable once InconsistentNaming
public sealed class iRacingService : IiRacingService
{
    private readonly iRacingApi _iracingApi;
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters = { new JsonNumberEnumConverter<iRacingTrackCategory>() },
        MaxDepth = 20,
        PropertyNameCaseInsensitive = true
    };

    public iRacingService(iRacingApi api, IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        
        _iracingApi = api;
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IEnumerable<iRacingTrack>> GetAllTracks()
    {
        return await PerformGetQuery<iRacingTrack>(_iracingApi.GetAllTracks).ConfigureAwait(false);
    }

    public async Task<IEnumerable<iRacingCar>> GetAllCars()
    {
        return await PerformGetQuery<iRacingCar>(_iracingApi.GetAllCars).ConfigureAwait(false);
    }
    
    public async Task<IEnumerable<iRacingCarClass>> GetAllCarClasses()
    {
        return await PerformGetQuery<iRacingCarClass>(_iracingApi.GetAllCarClasses).ConfigureAwait(false);
    }

    private delegate Task<ApiResponse<LinkResponse>> ApiGetter();
    
    private async Task<IEnumerable<T>> PerformGetQuery<T>(ApiGetter getter)
    {
        var dataResponse = await getter().ConfigureAwait(false);
        if (!dataResponse.IsSuccessStatusCode)
        {
            throw new InvalidDataException();
        }

        using var httpClient = _httpClientFactory.CreateClient();
        var linkUrl = new Uri(dataResponse.Content.Link);

        var response = await httpClient.GetAsync(linkUrl).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidDataException();
        }

        var body = await response.Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);

        return JsonSerializer.Deserialize<IEnumerable<T>>(body, _serializerOptions) ?? new List<T>();
    }
}