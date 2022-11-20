namespace Api.Chat;

public sealed class SendMessageRequest
{
	public required string Ip { get; set; }
	public required string Text { get; set; }
}