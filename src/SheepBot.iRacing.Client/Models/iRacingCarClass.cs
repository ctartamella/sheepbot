namespace SheepBot.iRacing.Client.Models;

// ReSharper disable once InconsistentNaming
public record iRacingCarClass
{
    public int CarClassId { get; init; }
    public string Name { get; init; } = null!;
    public int RelativeSpeed { get; init; }
    public string ShortName { get; init; } = null!;
    public List<iRacingCarInClass> CarsInClass { get; init; } = new();
}