namespace LogBook.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddLogEntryEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogEntries",
                c => new
                {
                    LogEntryId = c.Int(nullable: false, identity: true),
                    LogType = c.Int(nullable: false),
                    HostName = c.String(nullable: false),
                    Source = c.String(),
                    Message = c.String(),
                    UserName = c.String(),
                })
                .PrimaryKey(t => t.LogEntryId);
        }

        public override void Down()
        {
            DropTable("dbo.LogEntries");
        }
    }
}