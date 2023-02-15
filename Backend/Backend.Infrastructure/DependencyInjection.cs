using Backend.Application.Account;
using Backend.Application.Authentication.Interfaces;
using Backend.Application.Common.Services;
using Backend.Infrastructure.Account;
using Backend.Infrastructure.Authentication;
using Backend.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}