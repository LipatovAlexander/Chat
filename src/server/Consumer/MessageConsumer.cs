using Domain.Entities;
using Infrastructure;
using MassTransit;

namespace Consumer;

public sealed class MessageConsumer : IConsumer<Message>
{
	private readonly ApplicationDbContext _dbContext;

	public MessageConsumer(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Consume(ConsumeContext<Message> context)
	{
		_dbContext.Messages.Add(context.Message);
		await _dbContext.SaveChangesAsync();
	}
}