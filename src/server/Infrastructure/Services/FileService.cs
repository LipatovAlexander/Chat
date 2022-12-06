using Amazon.S3;
using Amazon.S3.Model;
using Infrastructure.Configurations.Settings;
using Microsoft.Extensions.Options;
using File = Domain.Entities.File;

namespace Infrastructure.Services;

public interface IFileService
{
	Task SaveToTempBucketAsync(File file);
	Task MoveToPersistentBucketAsync(string fileId);
	Task<File> GetAsync(string id);
}

public sealed class FileService : IFileService
{
	private readonly IAmazonS3 _s3;
	private readonly AmazonS3Settings _s3Settings;

	public FileService(IAmazonS3 s3, IOptions<AmazonS3Settings> s3SettingsOptions)
	{
		_s3 = s3;
		_s3Settings = s3SettingsOptions.Value;
	}

	public async Task SaveToTempBucketAsync(File file)
	{
		var bucket = _s3Settings.TempBucketName;
		
		if (!await _s3.DoesS3BucketExistAsync(bucket))
		{
			await _s3.PutBucketAsync(bucket);
		}

		file.Id = Guid.NewGuid().ToString();

		var request = new PutObjectRequest
		{
			BucketName = bucket,
			Key = file.Id,
			ContentType = file.ContentType,
			InputStream = file.ContentStream
		};

		await _s3.PutObjectAsync(request);
	}

	public async Task MoveToPersistentBucketAsync(string fileId)
	{
		if (!await _s3.DoesS3BucketExistAsync(_s3Settings.PersistentBucketName))
		{
			await _s3.PutBucketAsync(_s3Settings.PersistentBucketName);
		}

		await _s3.CopyObjectAsync(_s3Settings.TempBucketName, fileId, _s3Settings.PersistentBucketName, fileId);
		await _s3.DeleteObjectAsync(new DeleteObjectRequest
		{
			Key = fileId,
			BucketName = _s3Settings.TempBucketName
		});
	}

	public async Task<File> GetAsync(string id)
	{
		var request = new GetObjectRequest
		{
			BucketName = _s3Settings.PersistentBucketName,
			Key = id
		};

		var response = await _s3.GetObjectAsync(request);
		
		return new File
		{
			Id = response.Key,
			ContentStream = response.ResponseStream,
			ContentType = response.Headers.ContentType
		};
	}
}