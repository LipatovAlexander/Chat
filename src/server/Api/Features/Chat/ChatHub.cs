using System.Security.Claims;
using Domain.Events;
using Infrastructure;
using Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Chat;

public sealed class ChatHub : Hub
{
	private readonly IBus _bus;
	private readonly ICacheService _cacheService;
	private readonly IChatConnector _chatConnector;
	private readonly ApplicationDbContext _dbContext;

	public ChatHub(IBus bus, ICacheService cacheService, IChatConnector chatConnector, ApplicationDbContext dbContext)
	{
		_bus = bus;
		_cacheService = cacheService;
		_chatConnector = chatConnector;
		_dbContext = dbContext;
	}

	public override async Task OnConnectedAsync()
	{
		await _chatConnector.ConnectAsync(GetUsername(), Context.ConnectionId);
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await _chatConnector.DisconnectAsync(GetUsername(), Context.ConnectionId);
	}

	public async Task SendMessage(SendMessageRequest request)
	{
		var user = await _dbContext.Users
			.Include(u => u.ChatMate)
			.FirstAsync(u => u.Username == GetUsername());
		
		var message = request.MapToMessage(user);

		var messageCreated = new MessageCreatedEvent(message);
		await _bus.Publish(messageCreated);

		await Clients.Users(user.Username, user.ChatMate!.Username).SendAsync("ReceiveMessage", message);
	}

	public async Task Upload(string requestId)
	{
		await _cacheService.SaveConnectionId(requestId, Context.ConnectionId);
	}

	private string GetUsername()
	{
		return Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)!;
	}
}