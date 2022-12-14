namespace Infrastructure.Configurations.Settings;

public sealed class JwtSettings : ISettings
{
	public static string SectionName => "Jwt";

	public required string Issuer { get; set; }
	public required string Audience { get; set; }
	public required string Key { get; set; }
}