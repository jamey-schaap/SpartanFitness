using ErrorOr;

namespace GitHubFitness.Domain.Common.Errors;

public static partial class Errors {
    public static class MuscleGroup {
        public static Error NotFound => Error.NotFound(
            code: "MuscleGroup.NotFound",
            description: "MuscleGroup with given id does not exist");
    }
}