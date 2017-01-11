using System;
using System.Collections.Generic;
using System.Linq;

using DocumentDb.Sample.Documents.Position;

namespace DocumentDb.Sample.Documentsgfdsf
{
    public static class GenerateTestData
    {



        public static Positions GetCaseElementPosition(int count)
        {
            Positions poss = new Positions{CaseElementPositions =  new List<CaseElementPosition>(),CaseId = 26};
            Random r = new Random();
            
            foreach (var x in Enumerable.Range(1, count))
            {
                var pos = new Position {X = r.NextDouble(), Y = r.NextDouble()};
                ElementPosition elementPosition = new ElementPosition {ElementId = x, Position = pos};
                CaseElementPosition caseElementPosition = new CaseElementPosition {CaseId = 26, ElementPosition = elementPosition};
                poss.CaseElementPositions.Add(caseElementPosition);
            }
            return poss;
        }
    }
}