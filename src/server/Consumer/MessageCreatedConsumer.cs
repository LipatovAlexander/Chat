using Domain.Entities;
using Domain.Events;
using Infrastructure;
using MassTransit;

namespace Consumer;

public sealed class MessageCreatedConsumer : IConsumer<MessageCreatedEvent>
{
	private readonly ApplicationDbContext _dbContext;

	public MessageCreatedConsumer(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Consume(ConsumeContext<MessageCreatedEvent> context)
	{
		var message = MapToMessage(context.Message);
		_dbContext.Messages.Add(message);
		await _dbContext.SaveChangesAsync();
	}

	private static Message MapToMessage(MessageCreatedEvent createdEvent) => new()
	{
		Id = createdEvent.Id,
		Text = createdEvent.Text,
		FileId = createdEvent.FileId,
		CreatedAt = createdEvent.CreatedAt,
		SenderUsername = createdEvent.SenderUsername,
		ReceiverUsername = createdEvent.ReceiverUsername
	};
}