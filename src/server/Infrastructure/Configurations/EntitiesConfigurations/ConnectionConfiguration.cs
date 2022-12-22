using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntitiesConfigurations;

public sealed class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
	public void Configure(EntityTypeBuilder<Connection> builder)
	{
		builder.ToTable("Connection");

		builder.HasKey(connection => connection.Id);

		builder
			.HasOne(connection => connection.User)
			.WithMany(user => user.Connections)
			.HasForeignKey("UserUsername");
	}
}