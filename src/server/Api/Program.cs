using Api.Chat;
using Infrastructure.Configurations;
using Api.Extensions;
using Infrastructure.Configurations.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var connectionStringSettings = config.GetSettings<ConnectionStringsSettings>();
var brokerSettings = config.GetSettings<BrokerSettings>();
var amazonS3Settings = config.GetSettings<AmazonS3Settings>();

services.AddControllers()
	.ConfigureApiBehaviorOptions(options => options.SuppressInferBindingSourcesForParameters = true);
services.AddSignalR();
services.AddMassTransit(brokerSettings);
services.AddAmazonS3(amazonS3Settings);
services.AddFileService();

services.AddCors();

services.AddApplicationDbContext(connectionStringSettings);

var app = builder.Build();

app.UseCorsForFrontend(config);

app.UseFileServer();

app.MapControllers();
app.MapHub<ChatHub>("/api/chat");

app.Run();