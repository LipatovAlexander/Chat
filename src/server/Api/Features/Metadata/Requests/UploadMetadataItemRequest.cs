namespace Api.Features.Metadata.Requests;

public sealed class UploadMetadataItemRequest
{
	public required string Name { get; set; }
	public required string Value { get; set; }
}