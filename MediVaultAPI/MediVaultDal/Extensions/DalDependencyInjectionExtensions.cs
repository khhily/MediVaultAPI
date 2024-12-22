using System.Reflection;
using MediVaultDal;
using MediVaultDal.Account;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DalDependencyInjectionExtensions
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddTransient<UnitOfWork>();

        var types = Assembly.GetExecutingAssembly().GetExportedTypes().Where(q =>
            q is { IsAbstract: false, IsPublic: true, IsClass: true, IsInterface: false } && q.Name.EndsWith("Dal"));
        // services.AddTransient<UserDal>();
        // services.AddTransient<UserSessionDal>();

        foreach (var t in types)
        {
            services.AddTransient(t);

            if (t.BaseType != null && t.BaseType != typeof(object))
            {
                services.AddTransient(t.BaseType, t);
            }

            var interfaces = t.GetInterfaces();

            if (interfaces.Length == 0) continue;

            foreach (var interfaceType in interfaces)
            {
                services.AddTransient(interfaceType, t);
            }
        }
        return services;
    }
}
