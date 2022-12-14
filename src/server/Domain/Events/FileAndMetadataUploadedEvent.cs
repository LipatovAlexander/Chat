using Domain.Common;

namespace Domain.Events;

public sealed class FileAndMetadataUploadedEvent : BaseEvent
{
	public FileAndMetadataUploadedEvent(string requestId)
	{
		RequestId = requestId;
	}

	public string RequestId { get; }
}