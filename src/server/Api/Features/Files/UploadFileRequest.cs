using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Files;

public sealed class UploadFileRequest
{
	public required string Id { get; set; }
	
	[FromForm(Name = "file")]
	public required IFormFile FormFile { get; set; }
}