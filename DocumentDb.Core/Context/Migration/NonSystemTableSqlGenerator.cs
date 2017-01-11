using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.SqlServer;

namespace DocumentDb.Core.Context.Migration
{
    /// <summary>
    /// Класс создан для того чтобы запретить создавать системные таблицы (например таблицу миграции)
    /// </summary>
    public class NonSystemTableSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void GenerateMakeSystemTable(CreateTableOperation createTableOperation, IndentedTextWriter writer)
        {
        }
    }
}