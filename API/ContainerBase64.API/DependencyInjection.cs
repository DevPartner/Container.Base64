using ContainerBase64.API.SignalRHub;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", builder =>
            {
                var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
                builder.WithOrigins(allowedOrigins!)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .SetIsOriginAllowed((host) => true);
            });
        });

        services.AddControllers();
        services.AddSignalR();
        return services;
    }
}
