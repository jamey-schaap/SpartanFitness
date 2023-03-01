using ErrorOr;

using MediatR;

using SpartanFitness.Application.Authentication.Common;
using SpartanFitness.Application.Common.Interfaces.Authentication;
using SpartanFitness.Application.Common.Interfaces.Persistence;
using SpartanFitness.Application.Common.Interfaces.Services;
using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.Common.Errors;

namespace SpartanFitness.Application.Authentication.Queries;

public class LoginQueryHandler
    : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRoleManager _roleManager;

    public LoginQueryHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IRoleManager roleManager)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _roleManager = roleManager;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        LoginQuery query,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (!_passwordHasher.VerifyPassword(query.Password, user.Password, user.Salt))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var roles = await _roleManager.GetByUserIdAsync(user.Id);

        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        return new AuthenticationResult(
            user,
            token);
    }
}