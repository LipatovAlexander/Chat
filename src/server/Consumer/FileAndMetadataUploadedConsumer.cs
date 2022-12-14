using Domain.Events;
using Infrastructure.Services;
using MassTransit;

namespace Consumer;

public sealed class FileAndMetadataUploadedConsumer : IConsumer<FileAndMetadataUploadedEvent>
{
	private readonly IFileService _fileService;
	private readonly ICacheService _cacheService;
	private readonly IMetadataService _metadataService;
	private readonly IBus _bus;

	public FileAndMetadataUploadedConsumer(IFileService fileService, ICacheService cacheService, IMetadataService metadataService, IBus bus)
	{
		_fileService = fileService;
		_cacheService = cacheService;
		_metadataService = metadataService;
		_bus = bus;
	}

	public async Task Consume(ConsumeContext<FileAndMetadataUploadedEvent> context)
	{
		var requestId = context.Message.RequestId;
		var fileId = await _cacheService.GetFileIdAsync(requestId)
			?? throw new InvalidOperationException("FileId associated with passed RequestId not found in the cache");
		var metadata = await _cacheService.GetMetadataAsync(requestId)
			?? throw new InvalidOperationException("Metadata associated with passed RequestId not found in the cache");

		await _fileService.MoveToPersistentBucketAsync(fileId);
		await _metadataService.SaveMetadataAsync(fileId, metadata);

		var uploadFinished = new UploadFinishedEvent(requestId, fileId);
		await _bus.Publish(uploadFinished);
	}
}