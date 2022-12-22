namespace Api.Features.Chat;

public sealed class SendMessageRequest
{
	public required string Text { get; set; }
	public string? FileId { get; set; }
}