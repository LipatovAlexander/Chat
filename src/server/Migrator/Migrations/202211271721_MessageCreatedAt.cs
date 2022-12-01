using FluentMigrator;

namespace Migrator.Migrations;

[TimestampedMigration(2022, 11, 27, 17, 21)]
public sealed class MessageCreatedAt : AutoReversingMigration
{
	public override void Up()
	{
		Alter.Table("Message")
			.AddColumn("CreatedAt")
			.AsDateTimeOffset()
			.NotNullable()
			.SetExistingRowsTo(DateTimeOffset.UtcNow);
	}
}