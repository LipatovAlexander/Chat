using Infrastructure.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class AuthServiceConfiguration
{
	public static IServiceCollection AddAuthService(this IServiceCollection services)
	{
		return services.AddSingleton<IAuthService, AuthService>();
	}
}