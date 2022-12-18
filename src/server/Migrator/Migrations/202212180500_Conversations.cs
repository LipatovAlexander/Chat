using FluentMigrator;

namespace Migrator.Migrations;

[TimestampedMigration(2022, 12, 18, 5, 00)]
public sealed class Conversations : ForwardOnlyMigration
{
	public override void Up()
	{
		Create.Table("User")
			.WithColumn("Username").AsString().PrimaryKey()
			.WithColumn("ChatMateUsername").AsString().Nullable();

		Create.ForeignKey()
			.FromTable("User").ForeignColumn("ChatMateUsername")
			.ToTable("User").PrimaryColumn("Username");
	
		Create.Table("Connection")
			.WithColumn("Id").AsString().PrimaryKey()
			.WithColumn("UserUsername").AsString().ForeignKey("User", "Username");

		Delete.Column("Ip")
			.FromTable("Message");

		Alter.Table("Message")
			.AddColumn("SenderUsername").AsString().ForeignKey("User", "Username")
			.AddColumn("ReceiverUsername").AsString().ForeignKey("User", "Username");
	}
}