using File = Domain.Entities.File;

namespace Api.Features.Files;

public static class FileMapper
{
	public static File MapToFile(this IFormFile formFile) => new()
	{
		ContentStream = formFile.OpenReadStream(),
		ContentType = formFile.ContentType
	};
}