using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.Common.Models;

namespace SpartanFitness.Domain.Events;

public sealed record CoachApplicationApproved(CoachApplication coachApplication) : IDomainEvent
{
}