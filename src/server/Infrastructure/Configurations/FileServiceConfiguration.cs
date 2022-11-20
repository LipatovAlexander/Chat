using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class FileServiceConfiguration
{
	public static IServiceCollection AddFileService(this IServiceCollection services)
	{
		return services.AddSingleton<IFileService, FileService>();
	}
}