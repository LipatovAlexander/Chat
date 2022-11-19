using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Migrator.Migrations;

var configuration = CreateConfiguration();
var serviceProvider = CreateServices(configuration);

using var scope = serviceProvider.CreateScope();
UpdateDatabase(scope.ServiceProvider);

static IConfiguration CreateConfiguration()
{
	var config = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.Development.json", true)
		.AddEnvironmentVariables()
		.Build();

	return config;
}

static IServiceProvider CreateServices(IConfiguration config)
{
	var connectionString = config.GetConnectionString("Postgres");

	return new ServiceCollection()
		.AddFluentMigratorCore()
		.ConfigureRunner(rb => rb
			.AddPostgres()
			.WithGlobalConnectionString(connectionString)
			.ScanIn(typeof(MessageTable).Assembly).For.Migrations())
		.AddLogging(lb => lb.AddFluentMigratorConsole())
		.BuildServiceProvider(false);
}

static void UpdateDatabase(IServiceProvider serviceProvider)
{
	var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
	var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

	if (runner.HasMigrationsToApplyUp())
	{
		runner.MigrateUp();
	}
	else
	{
		logger.LogInformation("No migrations were applied. The database is already up to date");
	}
}