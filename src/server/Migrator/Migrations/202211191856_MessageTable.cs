using FluentMigrator;
using FluentMigrator.Postgres;

namespace Migrator.Migrations;

[TimestampedMigration(2022, 11, 19, 18, 56)]
public sealed class MessageTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Message")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("Ip").AsString(16).NotNullable()
            .WithColumn("Text").AsString().NotNullable();
    }
}