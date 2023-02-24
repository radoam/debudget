using Backend.Api.Common.Errors;
using Backend.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Backend.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, DebudgetProblemDetailsFactory>();
        services.AddMappings();

        return services;
    }
}