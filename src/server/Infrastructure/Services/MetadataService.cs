using System.Text.Json;
using Domain.Entities;
using Infrastructure.Configurations.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services;

public interface IMetadataService
{
	Task SaveMetadataAsync(string fileId, FileMetadata metadata);
	Task<FileMetadata?> GetMetadataAsync(string fileId);
}

public sealed class MetadataService : IMetadataService
{
	private readonly IMongoClient _mongo;
	private readonly MongoSettings _mongoSettings;

	public MetadataService(IMongoClient mongo, IOptions<MongoSettings> mongoSettingsOptions)
	{
		_mongo = mongo;
		_mongoSettings = mongoSettingsOptions.Value;
	}

	public async Task SaveMetadataAsync(string fileId, FileMetadata metadata)
	{
		var database = _mongo.GetDatabase(_mongoSettings.Database);
		var collection = database.GetCollection<BsonDocument>("metadata");

		var metadataJson = JsonSerializer.Serialize(metadata);
		var document = new BsonDocument
		{
			{ "fileId", fileId },
			{ "metadata", metadataJson }
		};

		await collection.InsertOneAsync(document);
	}

	public async Task<FileMetadata?> GetMetadataAsync(string fileId)
	{
		var database = _mongo.GetDatabase(_mongoSettings.Database);
		var collection = database.GetCollection<BsonDocument>("metadata");
		
		var filter = Builders<BsonDocument>.Filter.Eq("fileId", fileId);
		var document = await collection.Find(filter).FirstOrDefaultAsync();

		if (document is null)
		{
			return null;
		}
		
		var json = document["metadata"].AsString;
		
		return JsonSerializer.Deserialize<FileMetadata>(json)!;
	}
}