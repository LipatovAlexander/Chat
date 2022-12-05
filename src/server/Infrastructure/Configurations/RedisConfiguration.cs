using Infrastructure.Configurations.Settings;
using StackExchange.Redis;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class RedisConfiguration
{
	public static IServiceCollection AddRedis(this IServiceCollection services, RedisSettings settings)
	{
		var options = new ConfigurationOptions
		{
			EndPoints = new EndPointCollection { { settings.Host, settings.Port } },
			Password = settings.Password
		};
		var redis = ConnectionMultiplexer.Connect(options);

		return services.AddSingleton<IConnectionMultiplexer>(redis);
	}
}