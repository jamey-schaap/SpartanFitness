using ErrorOr;

using MediatR;

using SpartanFitness.Application.Common.Interfaces.Persistence;
using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.Common.Errors;
using SpartanFitness.Domain.ValueObjects;

namespace SpartanFitness.Application.Coaches.Queries.GetCoachById;

public class GetCoachByIdQueryHandler
    : IRequestHandler<GetCoachByIdQuery, ErrorOr<Coach>>
{
    private readonly ICoachRepository _coachRepository;

    public GetCoachByIdQueryHandler(ICoachRepository coachRepository)
    {
        _coachRepository = coachRepository;
    }

    public async Task<ErrorOr<Coach>> Handle(
        GetCoachByIdQuery query,
        CancellationToken cancellationToken)
    {
        var coachId = CoachId.Create(query.Id);

        if (await _coachRepository.GetByIdAsync(coachId) is not Coach coach)
        {
            return Errors.Coach.NotFound;
        }

        return coach;
    }
}