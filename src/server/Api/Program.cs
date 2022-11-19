using Api.Chat;
using Infrastructure.Configurations.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var connectionStringSettings = config
		.GetSection(ConnectionStringsSettings.SectionName)
		.Get<ConnectionStringsSettings>()
	?? throw new InvalidOperationException("Connection string settings not passed");
var brokerSettings = config
		.GetSection(BrokerSettings.SectionName)
		.Get<BrokerSettings>()
	?? throw new InvalidOperationException("Broker settings not passed");

services.AddControllers();
services.AddSignalR();
services.AddMassTransit(brokerSettings);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddApplicationDbContext(connectionStringSettings);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/api/chat");

app.Run();