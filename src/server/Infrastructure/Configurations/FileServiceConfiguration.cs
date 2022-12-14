using Infrastructure.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class FileServiceConfiguration
{
	public static IServiceCollection AddFileService(this IServiceCollection services)
	{
		return services.AddSingleton<IFileService, FileService>();
	}
}