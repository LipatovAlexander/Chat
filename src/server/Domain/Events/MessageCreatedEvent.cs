using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public sealed class MessageCreatedEvent : BaseEvent
{
	public MessageCreatedEvent(Message message)
	{
		Message = message;
	}

	public Message Message { get; }
}