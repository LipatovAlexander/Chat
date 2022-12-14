using Infrastructure.Configurations.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static void ConfigureSettings<TSettings>(this IServiceCollection services, TSettings settings)
		where TSettings: class, ISettings
	{
		var options = Options.Options.Create(settings);
		services.AddSingleton(options);
	}
}