using FluentValidation;

namespace SpartanFitness.Application.Workouts.Commands.CreateWorkout;

public class CreateWorkoutCommandValidator
 : AbstractValidator<CreateWorkoutCommand>
{
  public CreateWorkoutCommandValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .MaximumLength(100);

    RuleFor(x => x.Description)
      .NotEmpty()
      .MaximumLength(2048);

    RuleFor(x => x.Image)
      .NotEmpty()
      .MaximumLength(2048);

    RuleFor(x => x.CoachId)
      .Must(x => Guid.TryParse(x, out _))
      .WithMessage("The coach ID must be a valid GUID");

    RuleFor(x => x.Image)
      .NotEmpty();

    RuleForEach(x => x.WorkoutExercises)
      .SetValidator(new CreateWorkoutExerciseCommandValidator());

    RuleFor(x => x.WorkoutExercises)
      .Must(x =>
      {
        if (x == null)
        {
          return true;
        }

        var orderNumbers = x.ConvertAll(x => x.OrderNumber);

        orderNumbers.Sort();

        if (orderNumbers[0] != 1)
        {
          return false;
        }

        var consecutiveAndUnique = true;
        for (int i = 1; i < orderNumbers.Count; i++)
        {
          if (orderNumbers[i] != orderNumbers[i - 1] + 1)
          {
            consecutiveAndUnique = false;
            break;
          }
        }

        return consecutiveAndUnique;
      })
      .WithMessage("The order of exercises has to contain unique values, has to be consecutive and has to start with 1");
  }
}