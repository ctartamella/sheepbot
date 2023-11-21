using System.ComponentModel.DataAnnotations.Schema;

namespace SheepBot.Models;

[Table("[dbo].[class]")]
public sealed record Class : ModelBase
{
    // Properties
    public string Name { get; init; } = default!;

    // Relationships
    public List<Car> Cars { get; } = new();

    // IEquatable
    public bool Equals(Class? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}