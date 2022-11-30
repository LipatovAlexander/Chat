using Api.Configurations.Settings;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
{
	public static IApplicationBuilder UseCorsForFrontend(this IApplicationBuilder app, IConfiguration config)
	{
		var frontConfig = config
			                  .GetSection(FrontendSettings.SectionName)
			                  .Get<FrontendSettings>()
		                  ?? throw new InvalidOperationException("Frontend settings not passed");

		return app.UseCors(x =>
		{
			x.WithOrigins(frontConfig.Url);
			x.AllowAnyHeader();
			x.AllowCredentials();
			x.AllowAnyMethod();
		});
	}
}