using System.Data.Entity.Migrations;

namespace DocumentDb.Core.Context.Migration
{
    /// <summary>
    /// Когфигурация миграции структур данных Entity
    /// </summary>
    internal class DocumentDbContextMigration : DbMigrationsConfiguration<DocumentDbContext>
    {
        public DocumentDbContextMigration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("System.Data.SqlClient", new NonSystemTableSqlGenerator());
        }
    }
}