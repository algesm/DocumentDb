using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace DocumentDb.Core.Context
{
    internal class DocumentDbHistoryContext : HistoryContext
    {
        public DocumentDbHistoryContext(DbConnection dbConnection, string defaultSchema)
            : base(dbConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().ToTable("DocumentMigrationHistory");
        }
    }
}