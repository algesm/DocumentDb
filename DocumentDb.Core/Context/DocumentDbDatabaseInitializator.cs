using System.Data.Entity;
using DocumentDb.Core.Context.Migration;

namespace DocumentDb.Core.Context
{
    internal class DocumentDbDatabaseInitializator : MigrateDatabaseToLatestVersion<DocumentDbContext, DocumentDbContextMigration>
    {
    }
}