using System.Reflection;
using MediVaultData.Dto.Account;
using MediVaultService;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceDependencyInjectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration jwtConfiguration)
    {
        services.Configure<JwtOptions>(jwtConfiguration);

        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetExportedTypes().Where(q => q is { IsAbstract: false, IsInterface: false, IsPublic: true, IsClass: true } && q.Name.EndsWith("Service"));

        foreach (var t in types)
        {
            services.AddTransient(t);
            if (t.BaseType != null)
            {
                services.AddTransient(t.BaseType, t);
            }

            var interfaces = t.GetInterfaces();
            
            if (interfaces.Length <= 0) continue;
            
            foreach (var interfaceType in interfaces)
            {
                services.AddTransient(interfaceType, t);
            }
        }
        
        return services;
    }
}