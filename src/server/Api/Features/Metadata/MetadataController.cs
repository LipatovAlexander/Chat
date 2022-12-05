using Api.Features.Metadata.Requests;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Metadata;

[ApiController]
[Route("api/[controller]")]
public sealed class MetadataController : ControllerBase
{
	private readonly IMetadataService _metadataService;

	public MetadataController(IMetadataService metadataService)
	{
		_metadataService = metadataService;
	}

	[HttpPost]
	public async Task<IActionResult> UploadMetadata([FromBody]UploadMetadataRequest request)
	{
		var metadata = request.Metadata
			.Select(MetadataMapper.MapToMetadataItem)
			.ToArray();

		await _metadataService.SaveMetadataAsync(request.Id, metadata);

		return CreatedAtAction(nameof(GetMetadata), new { fileId = request.Id }, null);
	}

	[HttpGet]
	public async Task<IActionResult> GetMetadata(string fileId)
	{
		throw new NotImplementedException();
	}
}