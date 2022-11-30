using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.History;

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
		return await _dbContext.Messages
			.OrderBy(m => m.CreatedAt)
			.AsNoTracking()
			.ToListAsync();
	}
}