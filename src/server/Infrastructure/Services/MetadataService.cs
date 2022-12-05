using System.Text.Json;
using Domain.Entities;
using StackExchange.Redis;

namespace Infrastructure.Services;

public interface IMetadataService
{
	Task SaveMetadataAsync(string id, ICollection<MetadataItem> metadata);
}

public sealed class MetadataService : IMetadataService
{
	private readonly IConnectionMultiplexer _redis;

	public MetadataService(IConnectionMultiplexer redis)
	{
		_redis = redis;
	}

	public async Task SaveMetadataAsync(string id, ICollection<MetadataItem> metadata)
	{
		var database = _redis.GetDatabase();

		var json = JsonSerializer.Serialize(metadata);
		await database.HashSetAsync(id, "metadata", json);
	}
}