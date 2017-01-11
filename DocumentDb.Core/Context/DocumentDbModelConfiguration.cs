using System.Data.Entity;

namespace DocumentDb.Core.Context
{
    public class DocumentDbModelConfiguration : DbConfiguration
    {
        public DocumentDbModelConfiguration()
        {
            SetDefaultHistoryContext((connection, defaultSchema) => new DocumentDbHistoryContext(connection, defaultSchema));
            SetHistoryContext("System.Data.SqlClient",(connection, defaultSchema) => new DocumentDbHistoryContext(connection, defaultSchema));
        }
    }
}