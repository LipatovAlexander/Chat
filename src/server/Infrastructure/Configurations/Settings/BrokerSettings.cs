﻿namespace Infrastructure.Configurations.Settings;

public sealed class BrokerSettings
{
	public static readonly string SectionName = "Broker";

	public required string Host { get; set; }
	public required string Username { get; set; }
	public required string Password { get; set; }
}