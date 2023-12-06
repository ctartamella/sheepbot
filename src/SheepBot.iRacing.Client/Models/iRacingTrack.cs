using System.Text.Json.Serialization;

namespace SheepBot.iRacing.Client.Models;

// ReSharper disable once InconsistentNaming
public record iRacingTrack
{
    [JsonPropertyName("package_id")]
    public long TrackId { get; init; }
    public string TrackName { get; init; } = null!;
    public bool AiEnabled { get; init; }
    [JsonPropertyName("category_id")]
    public iRacingTrackCategory Category { get; init; }
    public bool FreeWithSubscription { get; init; }
    public bool FullyLit { get; init; }
    public bool IsDirt { get; init; }
    public bool IsOval { get; init; }
    public string? Location { get; init; }
    public float Latitude { get; init; }
    public float Longitude { get; init; }
    public short MaxCars { get; init; }
    public string? PriceDisplay { get; init; }
    public bool Purchasable { get; init; }
    public short PitRoadSpeedLimit { get; init; }
    public bool Retired { get; init; }
    
    // Track Config Data
    [JsonPropertyName("track_id")]
    public int ConfigId { get; set; }
    public int CornersPerLap { get; init; }
    public string ConfigName { get; init; } = null!;
    public float TrackConfigLength { get; init; }
}