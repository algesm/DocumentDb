using DocumentDb.Core.Indexes;

namespace DocumentDb.Sample.Indexes.Position
{
    public class Position1CaseIdIndex : Index
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
    }
}