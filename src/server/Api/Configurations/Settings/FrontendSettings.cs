using Infrastructure.Configurations.Settings;

namespace Api.Configurations.Settings;

public class FrontendSettings: ISettings
{
	public static string SectionName => "Frontend";
	
	public required string Url { get; set; }
}