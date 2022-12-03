using Amazon.Runtime;
using Amazon.S3;
using Infrastructure.Configurations;
using Infrastructure.Configurations.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class AmazonS3Configuration
{
	public static IServiceCollection AddAmazonS3(this IServiceCollection services, AmazonS3Settings settings)
	{
		services.ConfigureSettings(settings);
	
		var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
		var config = new AmazonS3Config
		{
			ForcePathStyle = true,
			ServiceURL = settings.ServiceUrl
		};
		var client = new AmazonS3Client(credentials, config);
		services.AddSingleton<IAmazonS3>(client);

		return services;
	}
}