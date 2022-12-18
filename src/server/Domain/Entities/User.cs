namespace Domain.Entities;

public sealed class User
{
	public required string Username { get; set; }

	public ICollection<Connection> Connections { get; set; } = default!;
	
	public User? ChatMate { get; set; }

	public ICollection<Message> SentMessages { get; set; } = default!;
	public ICollection<Message> ReceivedMessages { get; set; } = default!;

	public bool IsAdmin => Username.StartsWith("admin");
	
	public void AddConnection(string connectionId)
	{
		var connection = new Connection
		{
			Id = connectionId,
			User = this
		};

		Connections.Add(connection);
	}

	public void RemoveConnection(string connectionId)
	{
		var connection = Connections.First(c => c.Id == connectionId);

		Connections.Remove(connection);
	}
}