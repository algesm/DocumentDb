using System.Data.Entity;
using DocumentDb.Sample.Documents.Position;

namespace DocumentDb.Sample.TestContext
{
    public class PositionContext:DbContext
    {
        public PositionContext() :base("Data Source=localhost;Initial Catalog=test2;Integrated Security=true;")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public IDbSet<Position> Positions { get; set; }
        public IDbSet<ElementPosition> ElementPositions { get; set; }
        public IDbSet<CaseElementPosition> CaseElementPositions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PositionConfiguration());
            modelBuilder.Configurations.Add(new ElementPositionConfiguration());
            modelBuilder.Configurations.Add(new CaseElementPositionConfiguration());
        }
    }
}