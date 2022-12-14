using Domain.Events;
using Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Api.Features.Chat;

public sealed class ChatHub : Hub
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

	public async Task Upload(string requestId)
	{
		await _cacheService.SaveConnectionId(requestId, Context.ConnectionId);
	}
}