using LogBook.Entities.Migrations;
using System.Data.Entity.Migrations;

namespace LogBook.Entities
{
    public static class UpdateDatabase
    {
        public static void Execute()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}