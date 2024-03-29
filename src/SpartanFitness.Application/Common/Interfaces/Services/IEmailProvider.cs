using SpartanFitness.Domain.Aggregates;

namespace SpartanFitness.Application.Common.Interfaces.Services;

public interface IEmailProvider
{
  Task SendAsync(List<User> recipients, string subject, string body, CancellationToken cancellationToken);
}