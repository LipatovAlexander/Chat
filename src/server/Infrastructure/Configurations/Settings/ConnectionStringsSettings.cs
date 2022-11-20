namespace Infrastructure.Configurations.Settings;

public sealed class ConnectionStringsSettings
{
	public static readonly string SectionName = "ConnectionStrings";

	public required string Postgres { get; set; }
}