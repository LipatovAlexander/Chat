using Infrastructure.Configurations.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
	public static TSettings GetSettings<TSettings>(this IConfiguration configuration)
		where TSettings : ISettings
	{
		return configuration
				.GetSection(TSettings.SectionName)
				.Get<TSettings>()
			?? throw new InvalidOperationException($"{TSettings.SectionName} settings not passed");
	}
}