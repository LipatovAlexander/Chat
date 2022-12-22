using Infrastructure.Configurations.Settings;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MongoConfiguration
{
	public static IServiceCollection AddMongo(this IServiceCollection services, MongoSettings settings)
	{
		var connectionString =
			$"mongodb://{settings.Username}:{settings.Password}@{settings.Host}:{settings.Port}";
		var client = new MongoClient(connectionString);

		services.ConfigureSettings(settings);
		return services.AddSingleton<IMongoClient>(client);
	}
}