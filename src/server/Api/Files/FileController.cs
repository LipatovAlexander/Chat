using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Files;

[ApiController]
[Route("api/[controller]")]
public sealed class FileController : ControllerBase
{
	private readonly IFileService _fileService;

	public FileController(IFileService fileService)
	{
		_fileService = fileService;
	}

	[HttpPost]
	public async Task<IActionResult> SaveFile([FromForm(Name = "file")] IFormFile formFile)
	{
		var file = formFile.MapToFile();
	
		await _fileService.SaveAsync(file);

		var response = file.MapToResponse();

		return CreatedAtAction(nameof(GetFile), new {id = file.Id}, response);
	}

	[HttpGet]
	public async Task<IActionResult> GetFile(string id)
	{
		var file = await _fileService.GetAsync(id);

		return File(file.ContentStream, file.ContentType);
	}
}