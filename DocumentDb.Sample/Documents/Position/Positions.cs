using System.Collections.Generic;
using DocumentDb.Core.Model;

namespace DocumentDb.Sample.Documents.Position
{
    public class Positions : DocumentPrimaryKey
    {
        public ICollection<CaseElementPosition> CaseElementPositions { get; set; }
        public int CaseId { get; set; }
    }
}