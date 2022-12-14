using Domain.Common;

namespace Domain.Events;

public sealed class UploadFinishedEvent : BaseEvent
{
	public UploadFinishedEvent(string requestId, string fileId)
	{
		RequestId = requestId;
		FileId = fileId;
	}

	public string RequestId { get; }
	public string FileId { get; }
}