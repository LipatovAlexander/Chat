using Api.Features.Metadata.Requests;
using Domain.Entities;

namespace Api.Features.Metadata;

public static class MetadataMapper
{
	public static FileMetadata MapToFileMetadata(this UploadMetadataRequest request) => new()
	{
		Items = request.Metadata.Select(MapToFileMetadataItem).ToList()
	};

	private static FileMetadataItem MapToFileMetadataItem(this UploadMetadataItemRequest request) => new()
	{
		Name = request.Name,
		Value = request.Value
	};
}