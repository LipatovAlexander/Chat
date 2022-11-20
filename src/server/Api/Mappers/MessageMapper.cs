using Api.Chat;
using Domain.Entities;

namespace Api.Mappers;

public static class MessageMapper
{
	public static Message MapToMessage(this SendMessageRequest request)
	{
		return new Message
		{
			Ip = request.Ip,
			Text = request.Text
		};
	}
}