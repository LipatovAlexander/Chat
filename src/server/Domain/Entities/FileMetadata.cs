namespace Domain.Entities;

public sealed class FileMetadata
{
	public required IReadOnlyList<FileMetadataItem> Items { get; set; }
}

public sealed class FileMetadataItem
{
	public required string Name { get; set; }
	public required string Value { get; set; }
}