namespace Api.Configuration;

public static class CorsForFrontendConfiguration
{
	public static readonly string PolicyNameForFrontend = "Frontend";
	
	public static IServiceCollection AddCorsForFrontend(this IServiceCollection services)
	{
		return services.AddCors(options =>
		{
			options.AddPolicy(PolicyNameForFrontend,
				builder => builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
		});
	}
}