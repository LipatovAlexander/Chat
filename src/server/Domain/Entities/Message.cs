namespace Domain.Entities;

public sealed class Message
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public required string Text { get; set; }
	public string? FileId { get; set; }
	public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

	public User Sender { get; set; } = default!;
	public required string SenderUsername { get; set; }

	public User? Receiver { get; set; }
	public string? ReceiverUsername { get; set; }
}