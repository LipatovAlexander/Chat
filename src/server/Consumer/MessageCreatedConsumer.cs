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
		_dbContext.Messages.Add(context.Message.Message);
		await _dbContext.SaveChangesAsync();
	}
}