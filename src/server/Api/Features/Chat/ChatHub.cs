using Domain.Events;
using Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Api.Features.Chat;

public sealed class ChatHub : Hub, IConsumer<UploadFinishedEvent>
{
	private readonly IBus _bus;
	private readonly ICacheService _cacheService;

	public ChatHub(IBus bus, ICacheService cacheService)
	{
		_bus = bus;
		_cacheService = cacheService;
	}

	public async Task SendMessage(SendMessageRequest request)
	{
		var message = request.MapToMessage();

		var messageCreated = new MessageCreatedEvent(message);
		await _bus.Publish(messageCreated);

		await Clients.All.SendAsync("ReceiveMessage", message);
	}

	public async Task Upload(string requestId, string connectionId)
	{
		await _cacheService.SaveConnectionId(requestId, connectionId);
	}

	public override async Task OnConnectedAsync()
	{
		await Clients.Caller.SendAsync("ReceiveConnectionId", Context.ConnectionId);
	}

	public async Task Consume(ConsumeContext<UploadFinishedEvent> context)
	{
		var connectionId = await _cacheService.GetConnectionId(context.Message.RequestId)
			?? throw new InvalidOperationException("ConnectionId associated with the passed RequestId not found");

		await Clients.Client(connectionId).SendAsync("UploadFinished", new { fileId = context.Message.FileId });
	}
}