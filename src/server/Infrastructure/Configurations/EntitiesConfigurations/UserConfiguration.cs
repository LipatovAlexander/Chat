using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntitiesConfigurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("User");

		builder.HasKey(user => user.Username);

		builder
			.HasOne(user => user.ChatMate)
			.WithMany()
			.HasForeignKey("ChatMateUsername");
	}
}