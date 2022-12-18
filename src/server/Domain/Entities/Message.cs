namespace Domain.Entities;

public sealed class Message
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public required string Text { get; set; }
	public string? FileId { get; set; }
	public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

	public required User Sender { get; set; }
	
	public required User Receiver { get; set; }
}