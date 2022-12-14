using Consumer;
using Infrastructure.Configurations;
using Infrastructure.Configurations.Settings;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

var connectionStringSettings = config.GetSettings<ConnectionStringsSettings>();
var brokerSettings = config.GetSettings<BrokerSettings>();
var redisSettings = config.GetSettings<RedisSettings>();
var amazonS3Settings = config.GetSettings<AmazonS3Settings>();
var mongoSettings = config.GetSettings<MongoSettings>();

services.AddMassTransit(brokerSettings, configurator =>
{
	configurator.AddConsumers(typeof(MessageCreatedConsumer).Assembly);
});
services.AddApplicationDbContext(connectionStringSettings);
services.AddRedis(redisSettings);
services.AddAmazonS3(amazonS3Settings);
services.AddMongo(mongoSettings);

services.AddCacheService();
services.AddFileService();
services.AddMetadataService();

var app = builder.Build();

app.Run();