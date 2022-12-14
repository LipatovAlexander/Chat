using Api.Features.Metadata.Requests;
using Domain.Events;
using Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Metadata;

[ApiController]
[Route("api/[controller]")]
public sealed class MetadataController : ControllerBase
{
	private readonly IBus _bus;
	private readonly ICacheService _cacheService;
	private readonly IMetadataService _metadataService;

	public MetadataController(IBus bus, ICacheService cacheService, IMetadataService metadataService)
	{
		_bus = bus;
		_cacheService = cacheService;
		_metadataService = metadataService;
	}

	[HttpPost]
	public async Task<IActionResult> UploadMetadata([FromBody]UploadMetadataRequest request)
	{
		var fileMetadata = request.MapToFileMetadata();

		await _cacheService.SaveMetadataAsync(request.Id, fileMetadata);
		var fileMetadataUploaded = new FileMetadataUploadedEvent(request.Id);
		await _bus.Publish(fileMetadataUploaded);

		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetMetadata(string fileId)
	{
		var metadata = await _metadataService.GetMetadataAsync(fileId);
		return metadata is not null
			? Ok(metadata)
			: NotFound();
	}
}