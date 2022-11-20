using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table(nameof(Message))]
public sealed class Message
{
	public int Id { get; set; }
	public required string Ip { get; set; }
	public required string Text { get; set; }
	public string? FileId { get; set; }
}