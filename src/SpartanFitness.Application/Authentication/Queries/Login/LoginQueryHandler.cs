using ErrorOr;

using MediatR;

using SpartanFitness.Application.Authentication.Common;
using SpartanFitness.Application.Common.Interfaces.Authentication;
using SpartanFitness.Application.Common.Interfaces.Persistence;
using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.Common.Errors;
using SpartanFitness.Domain.ValueObjects;

namespace SpartanFitness.Application.Authentication.Queries.Login;

public class LoginQueryHandler
  : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IRoleRepository _roleRepository;
  private readonly IRefreshTokenRepository _refreshTokenRepository;

  public LoginQueryHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IRoleRepository roleRepository,
    IRefreshTokenRepository refreshTokenRepository)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
    _passwordHasher = passwordHasher;
    _roleRepository = roleRepository;
    _refreshTokenRepository = refreshTokenRepository;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(
    LoginQuery query,
    CancellationToken cancellationToken)
  {
    if (await _userRepository.GetByEmailAsync(query.Email) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    if (!_passwordHasher.VerifyPassword(query.Password, user.Password, user.Salt))
    {
      return Errors.Authentication.InvalidCredentials;
    }

    if (!user.EmailConfirmed)
    {
      return Errors.Authentication.EmailNotConfirmed;
    }

    var userId = UserId.Create(user.Id.Value);
    var roles = await _roleRepository.GetRolesByUserIdAsync(userId);

    var (accessToken, refreshToken) = _jwtTokenGenerator.GenerateTokenPair(user, roles);

    await _refreshTokenRepository.AddAsync(refreshToken);

    return new AuthenticationResult(
      user,
      accessToken,
      refreshToken,
      roles);
  }
}