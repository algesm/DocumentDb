using System.Data.Entity.ModelConfiguration;
using DocumentDb.Sample.Documents.Position;

namespace DocumentDb.Sample.TestContext
{
    public class CaseElementPositionConfiguration:EntityTypeConfiguration<CaseElementPosition>
    {
        public CaseElementPositionConfiguration()
        {
            ToTable("CaseElementPositions");
            HasKey(t => t.Id);
            Property(t => t.CaseId).IsRequired();
            HasRequired(t => t.ElementPosition).WithMany().WillCascadeOnDelete(true);
        }
    }
}