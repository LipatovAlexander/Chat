using Api.Configurations.Settings;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
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