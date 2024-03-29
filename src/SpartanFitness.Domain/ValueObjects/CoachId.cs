using SpartanFitness.Domain.Common.Models;

namespace SpartanFitness.Domain.ValueObjects;

public sealed class CoachId : AggregateRootId<Guid>
{
  public override Guid Value { get; protected set; }

  public CoachId(Guid value)
  {
    Value = value;
  }

  public static CoachId Create(Guid value)
  {
    return new(value);
  }

  public static CoachId Create(string value)
  {
    return new(Guid.Parse(value));
  }

  public static CoachId CreateUnique()
  {
    return new(Guid.NewGuid());
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}