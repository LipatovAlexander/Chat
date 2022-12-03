using Infrastructure.Configurations.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Configurations;

public static class ServiceCollectionExtensions
{
	public static void ConfigureSettings<TSettings>(this IServiceCollection services, TSettings settings)
		where TSettings: class, ISettings
	{
		var options = Options.Create(settings);
		services.AddSingleton(options);
	}
}