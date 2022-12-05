using Domain.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Api.Features.Chat;

public sealed class ChatHub : Hub
{
	private readonly IBus _bus;

	public ChatHub(IBus bus)
	{
		_bus = bus;
	}

	public async Task SendMessage(SendMessageRequest request)
	{
		var message = request.MapToMessage();

		var messageCreated = new MessageCreatedEvent(message);
		await _bus.Publish(messageCreated);

		await Clients.All.SendAsync("ReceiveMessage", message);
	}
}