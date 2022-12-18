using Domain.Entities;
using Domain.Events;

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

	public static MessageCreatedEvent MapToEvent(this Message message) => new()
	{
		Id = message.Id,
		Text = message.Text,
		FileId = message.FileId,
		CreatedAt = message.CreatedAt,
		SenderUsername = message.SenderUsername,
		ReceiverUsername = message.ReceiverUsername
	};
}