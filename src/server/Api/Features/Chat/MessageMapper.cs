using Domain.Entities;

namespace Api.Features.Chat;

public static class MessageMapper
{
	public static Message MapToMessage(this SendMessageRequest request, User sender) => new()
	{
		Text = request.Text,
		FileId = request.FileId,
		Sender = sender,
		SenderUsername = sender.Username,
		Receiver = sender.ChatMate!,
		ReceiverUsername = sender.ChatMate!.Username
	};
}