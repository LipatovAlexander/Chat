using File = Domain.Entities.File;

namespace Api.Features.Files;

public static class FileMapper
{
	public static File MapToFile(this IFormFile formFile) => new()
	{
		ContentStream = formFile.OpenReadStream(),
		ContentType = formFile.ContentType
	};

	public static UploadFileResponse MapToResponse(this File file) => new() { Id = file.Id };
}