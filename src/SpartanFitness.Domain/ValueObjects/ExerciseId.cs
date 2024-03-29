using SpartanFitness.Domain.Common.Models;

namespace SpartanFitness.Domain.ValueObjects;

public sealed class ExerciseId : AggregateRootId<Guid>
{
  public override Guid Value { get; protected set; }

  public ExerciseId(Guid value)
  {
    Value = value;
  }

  public static ExerciseId Create(Guid value)
  {
    return new(value);
  }

  public static ExerciseId Create(string value)
  {
    return new(Guid.Parse(value));
  }

  public static ExerciseId CreateUnique()
  {
    return new(Guid.NewGuid());
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}