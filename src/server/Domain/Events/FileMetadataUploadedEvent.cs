using Domain.Common;

namespace Domain.Events;

public sealed class FileMetadataUploadedEvent : BaseEvent
{
	public FileMetadataUploadedEvent(string requestId)
	{
		RequestId = requestId;
	}

	public string RequestId { get; }
}