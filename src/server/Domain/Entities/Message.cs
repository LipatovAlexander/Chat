using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table(nameof(Message))]
public sealed class Message
{
	public required int Id { get; set; }
	public required string Ip { get; set; }
	public required string Text { get; set; }
}