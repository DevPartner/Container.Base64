using ContainerBase64.Contracts.Services;
using ContainerBase64.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<ITaskManager<IEncodingClientProxy>, EncodingTaskManager>();
        services.AddSingleton<IEncodingService, EncodingService>();
        return services;
    }

}
