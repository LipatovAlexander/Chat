using FluentMigrator;

namespace Migrator.Migrations;

[TimestampedMigration(2022, 12, 21, 00, 31)]
public sealed class ReceiverUsernameNullable : AutoReversingMigration
{
	public override void Up()
	{
		Alter.Column("ReceiverUsername").OnTable("Message")
			.AsString()
			.Nullable();
	}
}