namespace Api.Features.Metadata.Requests;

public sealed class UploadMetadataRequest
{
	public required string Id { get; set; }

	public required IReadOnlyList<UploadMetadataItemRequest> Metadata { get; set; }
}