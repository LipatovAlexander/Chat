namespace Domain.Entities;

public sealed class Connection
{
	public required string Id { get; set; }

	public required User User { get; set; }
}