using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public interface IChatConnector
{
	Task ConnectAsync(string username, string connectionId);
	Task DisconnectAsync(string username, string connectionId);
}

public sealed class ChatConnector : IChatConnector
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IHubContext _hubContext;

	public ChatConnector(ApplicationDbContext dbContext, IHubContext hubContext)
	{
		_dbContext = dbContext;
		_hubContext = hubContext;
	}
	
	public async Task ConnectAsync(string username, string connectionId)
	{
		var user = await _dbContext.Users
			.Include(u => u.Connections)
			.Include(u => u.ChatMate)
			.FirstAsync(u => u.Username == username);

		user.AddConnection(connectionId);
		
		if (user.ChatMate is not null)
		{
			await _dbContext.SaveChangesAsync();
			return;
		}

		var chatMate = await _dbContext.Users
			.FirstOrDefaultAsync(u => u.IsAdmin != user.IsAdmin && u.ChatMate == null);

		if (chatMate is null)
		{
			return;
		}

		user.ChatMate = chatMate;
		await _dbContext.SaveChangesAsync();

		chatMate.ChatMate = user;
		await _dbContext.SaveChangesAsync();

		await _hubContext.Clients
			.Users(user.Username, user.ChatMate.Username)
			.SendAsync("JoinedRoom");
	}

	public async Task DisconnectAsync(string username, string connectionId)
	{
		var user = await _dbContext.Users
			.Include(u => u.Connections)
			.Include(u => u.ChatMate)
			.FirstAsync(u => u.Username == username);

		user.RemoveConnection(connectionId);

		if (user.Connections.Any())
		{
			await _dbContext.SaveChangesAsync();
			return;
		}

		if (user.ChatMate is not null)
		{
			await _hubContext.Clients.User(user.ChatMate.Username).SendAsync("LeftRoom");

			user.ChatMate.ChatMate = null;
			user.ChatMate = null;
			await _dbContext.SaveChangesAsync();
		}
	}
}