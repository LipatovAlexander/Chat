using Domain.Common;

namespace Domain.Events;

public sealed class FileUploadedEvent : BaseEvent
{
	public FileUploadedEvent(string requestId)
	{
		RequestId = requestId;
	}

	public string RequestId { get; }
}