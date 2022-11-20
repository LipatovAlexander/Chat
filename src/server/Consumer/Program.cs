using Consumer;
using Infrastructure.Configurations.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var connectionStringSettings = config.GetSettings<ConnectionStringsSettings>();
var brokerSettings = config.GetSettings<BrokerSettings>();

services.AddMassTransit(brokerSettings, configurator =>
{
	configurator.AddConsumer<MessageConsumer>();
});
services.AddApplicationDbContext(connectionStringSettings);

var app = builder.Build();

app.Run();