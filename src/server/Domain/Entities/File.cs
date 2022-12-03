namespace Domain.Entities;

public sealed class File
{
	public string Id { get; set; } = default!;
	public required Stream ContentStream { get; set; }
	public required string ContentType { get; set; }
}