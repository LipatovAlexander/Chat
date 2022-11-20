namespace Domain.Entities;

public sealed class Message
{
	public int Id { get; set; }
	public required string Ip { get; set; }
	public required string Text { get; set; }
}