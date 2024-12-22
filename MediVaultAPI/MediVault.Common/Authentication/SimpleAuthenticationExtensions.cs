using MediVault.Common.Authentication;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class SimpleAuthenticationExtensions
{
    public static IServiceCollection AddSimpleAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(o =>
        {
            o.AddScheme<SimpleAuthenticationHandler>(SimpleAuthenticationHandler.SchemeName,
                SimpleAuthenticationHandler.SchemeName);
        });
        return services;
    }
}