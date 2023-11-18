namespace SheepBot.Models;

public class CarClass : ModelBase
{
    public string Name { get; set; } = default!;

    public ICollection<Car> Cars { get; } = new List<Car>();
}