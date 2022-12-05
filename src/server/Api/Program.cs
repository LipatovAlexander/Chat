using Api.Extensions;
using Api.Features.Chat;
using Infrastructure.Configurations.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var connectionStringSettings = config.GetSettings<ConnectionStringsSettings>();
var brokerSettings = config.GetSettings<BrokerSettings>();
var amazonS3Settings = config.GetSettings<AmazonS3Settings>();
var redisSettings = config.GetSettings<RedisSettings>();

services.AddControllers()
	.ConfigureApiBehaviorOptions(options => options.SuppressInferBindingSourcesForParameters = true);
services.AddSignalR();
services.AddMassTransit(brokerSettings);
services.AddAmazonS3(amazonS3Settings);
services.AddRedis(redisSettings);

services.AddFileService();
services.AddMetadataService();

services.AddCors();

services.AddApplicationDbContext(connectionStringSettings);

var app = builder.Build();

app.UseCorsForFrontend(config);

app.MapControllers();
app.MapHub<ChatHub>("/api/chat");

app.Run();