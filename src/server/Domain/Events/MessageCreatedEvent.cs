using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public sealed class MessageCreatedEvent : BaseEvent
{
	public required string Id { get; set; }
	public required string Text { get; set; }
	public required string? FileId { get; set; }
	public required DateTimeOffset CreatedAt { get; set; }

	public required string SenderUsername { get; set; }

	public required string ReceiverUsername { get; set; }
}