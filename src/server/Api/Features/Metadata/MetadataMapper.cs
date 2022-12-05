using Api.Features.Metadata.Requests;
using Domain.Entities;

namespace Api.Features.Metadata;

public static class MetadataMapper
{
	public static MetadataItem MapToMetadataItem(this UploadMetadataItemRequest request) => new()
	{
		Name = request.Name,
		Value = request.Value
	};
}