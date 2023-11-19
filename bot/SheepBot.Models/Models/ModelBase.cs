namespace SheepBot.Models;

public abstract class ModelBase : IEquatable<ModelBase>
{
    public int Id { get; init; }

    public bool Equals(ModelBase? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ModelBase)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }

    public static bool operator ==(ModelBase? left, ModelBase? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ModelBase? left, ModelBase? right)
    {
        return !Equals(left, right);
    }
}