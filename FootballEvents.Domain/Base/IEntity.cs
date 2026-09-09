namespace FootballEvents.Domain.Base;

public interface IEntity : IAuditableEntity
{
    public long Id { get; }
}
