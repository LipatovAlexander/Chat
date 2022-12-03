using Domain.Entities;

namespace Api.Chat;

public static class MessageMapper
{
	public static Message MapToMessage(this SendMessageRequest request) => new()
	{
		Ip = request.Ip,
		Text = request.Text,
		FileId = request.FileId
	};
}