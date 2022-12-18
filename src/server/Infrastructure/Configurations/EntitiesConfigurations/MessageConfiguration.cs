using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EntitiesConfigurations;

public sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
	public void Configure(EntityTypeBuilder<Message> builder)
	{
		builder.ToTable("Message");

		builder
			.HasOne(message => message.Sender)
			.WithMany(user => user.SentMessages)
			.HasForeignKey("SenderUsername");

		builder
			.HasOne(message => message.Receiver)
			.WithMany(user => user.ReceivedMessages)
			.HasForeignKey("ReceiverUsername");
	}
}