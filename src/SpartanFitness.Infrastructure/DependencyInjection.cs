using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Quartz;

using SpartanFitness.Application.Common.Interfaces.Authentication;
using SpartanFitness.Application.Common.Interfaces.Persistence;
using SpartanFitness.Application.Common.Interfaces.Services;
using SpartanFitness.Infrastructure.Authentication;
using SpartanFitness.Infrastructure.Common.Frontend;
using SpartanFitness.Infrastructure.Configuration.BackgroundJobs;
using SpartanFitness.Infrastructure.Persistence;
using SpartanFitness.Infrastructure.Persistence.Interceptors;
using SpartanFitness.Infrastructure.Persistence.Repositories;
using SpartanFitness.Infrastructure.Services;

using Swashbuckle.AspNetCore.Filters;

namespace SpartanFitness.Infrastructure;

/// <summary>
/// Static DependencyInjection which will be injected in /SpartanFitness.Api/Program.cs.
/// </summary>
public static class DependencyInjection
{
  /// <summary>
  /// Adds the Infrastructure layer [Clean Architecture Layers].
  /// </summary>
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    services
      .AddAuth(configuration)
      .AddPersistence()
      .AddServices(configuration)
      .AddJobs();

    return services;
  }

  public static IServiceCollection AddPersistence(
    this IServiceCollection services)
  {
    services.AddDbContext<SpartanFitnessDbContext>(options =>
      options.UseSqlServer("Name=ConnectionStrings:SpartanFitness"));

    services.AddScoped<PublishDomainEventsInterceptor>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ICoachRepository, CoachRepository>();
    services.AddScoped<IRoleRepository, RoleRepository>();
    services.AddScoped<IAdministratorRepository, AdministratorRepository>();
    services.AddScoped<ICoachApplicationRepository, CoachApplicationRepository>();
    services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();
    services.AddScoped<IExerciseRepository, ExerciseRepository>();
    services.AddScoped<IWorkoutRepository, WorkoutRepository>();
    services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    services.AddScoped<IMuscleRepository, MuscleRepository>();
    services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();

    return services;
  }

  public static IServiceCollection AddAuth(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    var coachSettings = new CoachCreationSettings();
    configuration.Bind(CoachCreationSettings.SectionName, coachSettings);

    var passwordResetSettings = new PasswordResetSettings();
    configuration.Bind(PasswordResetSettings.SectionName, passwordResetSettings);

    var jwtSettings = new JwtSettings();
    configuration.Bind(JwtSettings.SectionName, jwtSettings);

    services.AddSingleton(Options.Create(coachSettings));
    services.AddSingleton(Options.Create(passwordResetSettings));
    services.AddSingleton(Options.Create(jwtSettings));

    services.AddSingleton<IEmailConfirmationTokenProvider, EmailConfirmationTokenProvider>();
    services.AddSingleton<ICoachCreationTokenProvider, CoachCreationTokenProvider>();
    services.AddSingleton<IPasswordResetTokenProvider, PasswordResetTokenProvider>();
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IPasswordHasher, PasswordHasher>();

    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = jwtSettings.Issuer,
      ValidAudience = jwtSettings.Audience,
      IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
    };

    services.AddSingleton(tokenValidationParameters);

    services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options => options.TokenValidationParameters = tokenValidationParameters);

    services.ConfigureSwaggerGen(options =>
    {
      options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
      {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
      });

      options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    return services;
  }

  public static IServiceCollection AddServices(
    this IServiceCollection services,
    ConfigurationManager configuration)
  {
    var emailSettings = new EmailSettings();
    configuration.Bind(EmailSettings.SectionName, emailSettings);

    var frontendSettings = new FrontendSettings();
    configuration.Bind(FrontendSettings.SectionName, frontendSettings);

    services.AddSingleton(Options.Create(emailSettings));
    services.AddSingleton(Options.Create(frontendSettings));
    services.AddSingleton<IEmailProvider, EmailProvider>();
    services.AddSingleton<IFrontendProvider, FrontendProvider>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    return services;
  }

  public static IServiceCollection AddJobs(
    this IServiceCollection services)
  {
    services.AddQuartz();
    services.AddQuartzHostedService(options =>
    {
      options.WaitForJobsToComplete = true;
    });

    services.ConfigureOptions<SpartanFitnessDbCleanupBackgroundJobConfiguration>();

    return services;
  }
}