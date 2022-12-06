using System.Text.Json;
using Domain.Entities;
using StackExchange.Redis;

namespace Infrastructure.Services;

public interface ICacheService
{
	Task SaveMetadataAsync(string requestId, FileMetadata metadata);
	Task<FileMetadata?> GetMetadataAsync(string requestId);
	Task SaveFileIdAsync(string requestId, string fileId);
	Task<string?> GetFileIdAsync(string requestId);
	Task<long> IncrementCounterAsync(string requestId);
	Task SaveConnectionId(string requestId, string connectionId);
	Task<string?> GetConnectionId(string requestId);
}

public sealed class CacheService : ICacheService
{
	private readonly IConnectionMultiplexer _redis;

	private const string MetadataFieldName = "metadata";
	private const string FileIdFieldName = "file-id";
	private const string CounterFieldName = "counter";
	private const string ConnectionIdFieldName = "connection-id";

	public CacheService(IConnectionMultiplexer redis)
	{
		_redis = redis;
	}

	public async Task SaveMetadataAsync(string requestId, FileMetadata metadata)
	{
		var database = _redis.GetDatabase();

		var json = JsonSerializer.Serialize(metadata);
		await database.HashSetAsync(requestId, MetadataFieldName, json);
	}

	public async Task<FileMetadata?> GetMetadataAsync(string requestId)
	{
		var database = _redis.GetDatabase();

		var value = await database.HashGetAsync(requestId, MetadataFieldName);
		
		return value.HasValue 
			? JsonSerializer.Deserialize<FileMetadata>(value.ToString()) 
			: null;
	}

	public async Task SaveFileIdAsync(string requestId, string fileId)
	{
		var database = _redis.GetDatabase();

		await database.HashSetAsync(requestId, FileIdFieldName, fileId);
	}

	public async Task<string?> GetFileIdAsync(string requestId)
	{
		var database = _redis.GetDatabase();

		return await database.HashGetAsync(requestId, FileIdFieldName);
	}

	public async Task<long> IncrementCounterAsync(string requestId)
	{
		var database = _redis.GetDatabase();

		return await database.HashIncrementAsync(requestId, CounterFieldName);
	}

	public async Task SaveConnectionId(string requestId, string connectionId)
	{
		var database = _redis.GetDatabase();

		await database.HashSetAsync(requestId, ConnectionIdFieldName, connectionId);
	}

	public async Task<string?> GetConnectionId(string requestId)
	{
		var database = _redis.GetDatabase();

		return await database.HashGetAsync(requestId, ConnectionIdFieldName);
	}
}