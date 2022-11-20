using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Api.Chat;

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

		await _bus.Publish(message);

		await Clients.All.SendAsync("ReceiveMessage", message);
	}
}