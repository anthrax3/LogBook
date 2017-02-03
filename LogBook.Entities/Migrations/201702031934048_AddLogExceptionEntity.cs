namespace LogBook.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddLogExceptionEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogExceptions",
                c => new
                {
                    LogExceptionId = c.Int(nullable: false, identity: true),
                    LogEntryId = c.Int(nullable: false),
                    ExceptionDetail = c.String(),
                })
                .PrimaryKey(t => t.LogExceptionId)
                .ForeignKey("dbo.LogEntries", t => t.LogEntryId, cascadeDelete: true)
                .Index(t => t.LogEntryId);

            AddColumn("dbo.LogEntries", "LogTime", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropForeignKey("dbo.LogExceptions", "LogEntryId", "dbo.LogEntries");
            DropIndex("dbo.LogExceptions", new[] { "LogEntryId" });
            DropColumn("dbo.LogEntries", "LogTime");
            DropTable("dbo.LogExceptions");
        }
    }
}