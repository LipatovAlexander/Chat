using System.Security.Claims;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.History;

[ApiController]
[Route("/api/[controller]")]
public sealed class HistoryController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;

	public HistoryController(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	public async Task<ActionResult<List<Message>>> GetHistory()
	{
		var username = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
		var user = await _dbContext.Users
			.Include(u => u.ChatMate)
			.FirstAsync(u => u.Username == username);

		if (user.IsAdmin)
		{
			return await GetHistoryAsync(user.ChatMate!);
		}

		return await GetHistoryAsync(user);
	}

	private async Task<List<Message>> GetHistoryAsync(User user)
	{
		return await _dbContext.Messages
			.Where(m => m.Sender.Username == user.Username || m.Receiver.Username == user.Username)
			.OrderBy(m => m.CreatedAt)
			.AsNoTracking()
			.ToListAsync();
	}
}