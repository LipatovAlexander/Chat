namespace Domain.Entities;

public sealed class Message
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public required string Ip { get; set; }
	public required string Text { get; set; }
}