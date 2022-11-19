using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Api.Chat;

public sealed class ChatHub : Hub
{
	public async Task SendMessageAsync(Message message)
	{
		await Clients.All.SendAsync("ReceiveMessage", message);
	}
}