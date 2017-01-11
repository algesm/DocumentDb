using System;
using DocumentDb.Core.Indexes;
using DocumentDb.Sample.Documents.Position;

namespace DocumentDb.Sample.Indexes.Position
{

    public class PositionCaseIndexDescriptor : IndexDescriptor<Positions, PositionCaseIdIndex>
    {
        protected override Action<Positions, PositionCaseIdIndex> Describe
        {
            get { return (source, dest) => dest.CaseId = source.CaseId; }
        }
    }
}