using GitHubFitness.Domain.Common.Interfaces;

namespace GitHubFitness.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
  private List<IDomainEvent> _domainEvents = new();
  public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
  public TId Id { get; protected set; }

  protected Entity(TId id)
  {
    this.Id = id;
  }

#pragma warning disable CS8618
  private protected Entity()
  {
  }
#pragma warning restore CS8618

  public override bool Equals(object? obj)
  {
    return obj is Entity<TId> entity && Id.Equals(entity.Id);
  }

  public bool Equals(Entity<TId>? other)
  {
    return Equals((object?)other);
  }

  public static bool operator ==(Entity<TId> left, Entity<TId> right)
  {
    return Equals(left, right);
  }

  public static bool operator !=(Entity<TId> left, Entity<TId> right)
  {
    return !Equals(left, right);
  }

  public override int GetHashCode()
  {
    return Id.GetHashCode();
  }

  public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
  public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
  public void ClearDomainEvents() => _domainEvents = new();
}