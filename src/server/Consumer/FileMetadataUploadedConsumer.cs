using Domain.Events;
using Infrastructure.Services;
using MassTransit;

namespace Consumer;

public sealed class FileMetadataUploadedConsumer : IConsumer<FileMetadataUploadedEvent>
{
	private readonly IBus _bus;
	private readonly ICacheService _cacheService;

	public FileMetadataUploadedConsumer(IBus bus, ICacheService cacheService)
	{
		_bus = bus;
		_cacheService = cacheService;
	}

	public async Task Consume(ConsumeContext<FileMetadataUploadedEvent> context)
	{
		var requestId = context.Message.RequestId;

		var counter = await _cacheService.IncrementCounterAsync(requestId);
		if (counter == 2)
		{
			var fileAndMetadataUploaded = new FileAndMetadataUploadedEvent(requestId);
			await _bus.Publish(fileAndMetadataUploaded);
		}
	}
}