using Domain.Events;
using Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Files;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public sealed class FileController : ControllerBase
{
	private readonly IFileService _fileService;
	private readonly IBus _bus;
	private readonly ICacheService _cacheService;

	public FileController(IFileService fileService, IBus bus, ICacheService cacheService)
	{
		_fileService = fileService;
		_bus = bus;
		_cacheService = cacheService;
	}

	[HttpPost]
	public async Task<IActionResult> SaveFile([FromForm] UploadFileRequest request)
	{
		var file = request.FormFile.MapToFile();
	
		await _fileService.SaveToTempBucketAsync(file);
		await _cacheService.SaveFileIdAsync(request.Id, file.Id);
		var fileUploaded = new FileUploadedEvent(request.Id);
		await _bus.Publish(fileUploaded);		

		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetFile(string id)
	{
		var file = await _fileService.GetAsync(id);

		return File(file.ContentStream, file.ContentType);
	}
}