namespace Infrastructure.Configurations.Settings;

public sealed class ConnectionStringsSettings : ISettings
{
	public static string SectionName => "ConnectionStrings";

	public required string Postgres { get; set; }
}