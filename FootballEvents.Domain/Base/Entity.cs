namespace FootballEvents.Domain.Base;

public abstract class Entity : IEquatable<Entity>, IEntity
{
    public long Id { get; private init; }

    protected Entity()
    {
    }

    public static bool operator ==(Entity? left, Entity? right) =>
            ReferenceEquals(left, right) || (left?.Id == right?.Id);


    public static bool operator !=(Entity? left, Entity? right) =>
        !(left == right);


    public override bool Equals(object? obj) =>
        obj is Entity entity && entity.Id == Id;


    public bool Equals(Entity? other) =>
        other is not null && other.Id == Id;

    public override int GetHashCode() => Id.GetHashCode();
}
