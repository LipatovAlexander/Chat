using Api.Features.Metadata.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Metadata;

[ApiController]
[Route("api/[controller]")]
public sealed class MetadataController : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> UploadMetadataAsync(UploadMetadataRequest request)
	{
		throw new NotImplementedException();
	}

	[HttpGet]
	public async Task<IAsyncResult> GetMetadataAsync(string fileId)
	{
		throw new NotImplementedException();
	}
}