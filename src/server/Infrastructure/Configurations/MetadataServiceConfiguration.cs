using Infrastructure.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MetadataServiceConfiguration
{
	public static IServiceCollection AddMetadataService(this IServiceCollection services)
	{
		return services.AddSingleton<IMetadataService, MetadataService>();
	}
}