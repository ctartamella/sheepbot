namespace SheepBot.Models;

public class Car : ModelBase
{
    public string Name { get; set; } = default!;
    public bool IsFree { get; set; }
    public bool IsLegacy { get; set; }

    public ICollection<CarClass> Classes { get; } = new List<CarClass>();
}