using FluentMigrator;

namespace Migrator.Migrations;

[TimestampedMigration(2022, 11, 20, 13, 16)]
public sealed class Message_FileId : AutoReversingMigration 
{
	public override void Up()
	{
		Alter.Table("Message")
			.AddColumn("FileId").AsString(36).Nullable();
	}
}