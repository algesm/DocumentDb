using DocumentDb.Core.Indexes;

namespace DocumentDb.Sample.Indexes.Position
{
    public class PositionCaseIdIndex : Index
    {
        public int CaseId { get; set; }
    }
}
