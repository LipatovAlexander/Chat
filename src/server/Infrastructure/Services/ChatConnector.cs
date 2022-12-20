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
		var user = await GetUserAsync(username);

		user.AddConnection(connectionId);
		
		if (user.ChatMate is not null)
		{
			await _dbContext.SaveChangesAsync();
			return;
		}

		var chatMate = await GetFreeUserAsync(!user.IsAdmin);

		if (chatMate is null)
		{
			await _dbContext.SaveChangesAsync();
			return;
		}

		await ConnectUsersAsync(user, chatMate);
	}

	public async Task DisconnectAsync(string username, string connectionId)
	{
		var user = await GetUserAsync(username);

		user.RemoveConnection(connectionId);

		if (user.Connections.Any())
		{
			await _dbContext.SaveChangesAsync();
			return;
		}

		if (user.ChatMate is null)
		{
			return;
		}

		var chatMate = user.ChatMate;
		await DisconnectUsersAsync(user, chatMate);

		var newChatMate = await GetFreeUserAsync(!chatMate.IsAdmin);
		
		if (newChatMate is not null)
		{
			await ConnectUsersAsync(chatMate, newChatMate);
		}
	}

	private async Task DisconnectUsersAsync(User user1, User user2)
	{
		await _hubContext.Clients.Users(user1.Username, user2.Username).SendAsync("LeftRoom");

		user1.ChatMate = null;
		user2.ChatMate = null;
		await _dbContext.SaveChangesAsync();
	}

	private async Task<User> GetUserAsync(string username)
	{
		return await _dbContext.Users
			.Include(u => u.Connections)
			.Include(u => u.ChatMate)
			.FirstAsync(u => u.Username == username);
	}

	private async Task<User?> GetFreeUserAsync(bool admin)
	{
		return (await _dbContext.Users
				.Where(u => u.ChatMate == null && u.Connections.Any())
				.ToListAsync())
			.FirstOrDefault(u => u.IsAdmin == admin);
	}

	private async Task ConnectUsersAsync(User user1, User user2)
	{
		user1.ChatMate = user2;
		await _dbContext.SaveChangesAsync();

		user2.ChatMate = user1;
		await _dbContext.SaveChangesAsync();

		await _hubContext.Clients
			.Users(user1.Username, user2.Username)
			.SendAsync("JoinedRoom");
	}
}