using SheepBot.Models.Enums;

namespace SheepBot.Models;

public class Race : ModelBase
{
    public Series? Series { get; set; }
    public Track Track { get; set; } = default!;
    public DateTimeOffset? PracticeTime { get; set; }
    public DateTimeOffset QualiTime { get; set; }
    public int? Length { get; set; }
    public RaceLengthUnit? Unit { get; set; }
}