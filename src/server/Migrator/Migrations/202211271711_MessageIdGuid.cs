using FluentMigrator;

namespace Migrator.Migrations;

[TimestampedMigration(2022, 11, 27, 17, 11)]
public sealed class MessageIdGuid : AutoReversingMigration
{
	public override void Up()
	{
		Alter.Column("Id").OnTable("Message")
			.AsString(36)
			.NotNullable()
			.PrimaryKey();
	}
}