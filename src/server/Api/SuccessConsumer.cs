using System.Diagnostics;
using Api.Features.Chat;
using Domain.Events;
using Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Api;

public class SuccessConsumer: IConsumer<UploadFinishedEvent>
{
	private readonly IHubContext<ChatHub> _hubContext;
	private readonly ICacheService _cacheService;
	
	public SuccessConsumer(IHubContext<ChatHub> hubContext, ICacheService cacheService)
	{
		_hubContext = hubContext;
		_cacheService = cacheService;
	}
	
	public async Task Consume(ConsumeContext<UploadFinishedEvent> context)
	{
		var connectionId = await _cacheService.GetConnectionId(context.Message.RequestId)
		                   ?? throw new InvalidOperationException("ConnectionId associated with the passed RequestId not found");

		await _hubContext.Clients.Client(connectionId).SendAsync("UploadFinished", new { fileId = context.Message.FileId });
	}
}