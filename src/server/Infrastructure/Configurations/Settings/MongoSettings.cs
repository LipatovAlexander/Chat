namespace Infrastructure.Configurations.Settings;

public sealed class MongoSettings : ISettings
{
	public static string SectionName => "Mongo";

	public required string Username { get; set; }
	public required string Password { get; set; }
	public required string Host { get; set; }
	public required int Port { get; set; }

	public required string Database { get; set; }
}