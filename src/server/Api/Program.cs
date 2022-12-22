using Api.Features.Chat;
using Infrastructure.Configurations.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var connectionStringSettings = config.GetSettings<ConnectionStringsSettings>();
var brokerSettings = config.GetSettings<BrokerSettings>();
var amazonS3Settings = config.GetSettings<AmazonS3Settings>();
var redisSettings = config.GetSettings<RedisSettings>();
var mongoSettings = config.GetSettings<MongoSettings>();
var jwtSettings = config.GetSettings<JwtSettings>();

services.AddControllers()
	.ConfigureApiBehaviorOptions(options => options.SuppressInferBindingSourcesForParameters = true);
services.AddSignalR();
services.AddMassTransit(brokerSettings, configurator =>
{
	configurator.AddConsumer<UploadFinishedConsumer>();
});
services.AddAmazonS3(amazonS3Settings);
services.AddRedis(redisSettings);
services.AddMongo(mongoSettings);
services.AddJwtAuthentication(jwtSettings);

services.AddFileService();
services.AddCacheService();
services.AddMetadataService();
services.AddAuthService();
services.AddChatConnector<ChatHub>();

services.AddCors();

services.AddApplicationDbContext(connectionStringSettings);

var app = builder.Build();

app.UseCorsForFrontend(config);
app.UseJwtAuthentication();

app.MapControllers();
app.MapHub<ChatHub>("/api/chat");

app.Run();