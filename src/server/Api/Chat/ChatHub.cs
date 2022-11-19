using Domain.Entities;
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

	public async Task SendMessageAsync(Message message, CancellationToken cancellationToken = default)
	{
		await _bus.Publish(message, cancellationToken);

		await Clients.All.SendAsync("ReceiveMessage", message, cancellationToken);
	}
}