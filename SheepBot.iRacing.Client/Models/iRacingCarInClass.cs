namespace SheepBot.iRacing.Client.Models;

// ReSharper disable once InconsistentNaming
public record iRacingCarInClass
{
    public int CarId { get; init; }
    public bool Retired { get; init; }
}