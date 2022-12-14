using Infrastructure.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class CacheServiceConfiguration
{
	public static IServiceCollection AddCacheService(this IServiceCollection services)
	{
		return services.AddSingleton<ICacheService, CacheService>();
	}
}