namespace Infrastructure.Configurations.Settings;

public sealed class RedisSettings : ISettings
{
	public static string SectionName => "Redis";

	public required string Password { get; set; }

	public required string Host { get; set; }

	public required int Port { get; set; }

	public required string MetadataFieldName { get; set; }
	public required string CounterFieldName { get; set; }
}