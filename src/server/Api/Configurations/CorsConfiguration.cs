using Api.Configurations.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class CorsConfiguration
{
	public static IApplicationBuilder UseCorsForFrontend(this IApplicationBuilder app, IConfiguration config)
	{
		var frontConfig = config.GetSettings<FrontendSettings>();

		return app.UseCors(x =>
		{
			x.WithOrigins(frontConfig.Url);
			x.AllowAnyHeader();
			x.AllowCredentials();
			x.AllowAnyMethod();
		});
	}
}