namespace Infrastructure.Configurations.Settings;

public sealed class AmazonS3Settings : ISettings
{
	public static string SectionName => "AmazonS3";

	public required string ServiceUrl { get; set; }
	
	public required string AccessKey { get; set; }
	public required string SecretKey { get; set; }
	
	public required string TempBucketName { get; set; }
	public required string PersistentBucketName { get; set; }
}