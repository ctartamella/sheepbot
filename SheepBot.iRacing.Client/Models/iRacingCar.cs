namespace SheepBot.iRacing.Client.Models;

// ReSharper disable once InconsistentNaming
public record iRacingCar
{
    public int CarId { get; init; }
    public string CarName { get; init; } = null!;
    public bool FreeWithSubscription { get; init; }
}