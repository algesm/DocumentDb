using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DocumentDb.Sample.Documents.Position;

namespace DocumentDb.Sample.TestContext
{
    public class ElementPositionConfiguration : EntityTypeConfiguration<ElementPosition>
    {
        public ElementPositionConfiguration()
        {
            ToTable("ElementPositions");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ElementId).IsRequired();
            HasRequired(t => t.Position).WithMany().WillCascadeOnDelete(true);
        }
    }
}