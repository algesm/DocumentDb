using System;
using System.Collections.Generic;
using DocumentDb.Core.Indexes;
using DocumentDb.Core.Reflection;
using DocumentDb.Core.Serialization;

namespace DocumentDb.Core.Services
{
    public class DocumentDbSessionFactory : IDocumentDbSessionFactory
    {
        private readonly IDocumentSerializerFactory _documentSerializerFactory;
        private readonly HashSet<IDocumentDbSession> _sessions;
        private readonly Type _typeIndexDescription;

        public DocumentDbSessionFactory(IDocumentSerializerFactory documentSerializerFactory)
        {
            _documentSerializerFactory = documentSerializerFactory;
            _sessions = new HashSet<IDocumentDbSession>();
            _typeIndexDescription = typeof (IndexDescriptor<,>);
        }


        public IDocumentDbSession CreateSession()
        {
            IDocumentDbSession session = new DocumentDbSession( _typeIndexDescription, _documentSerializerFactory.Create(DocumentSerializer.JsonNet));
            _sessions.Add(session);
            return session;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IDocumentDbSession dbSession in _sessions)
                {
                    dbSession.Dispose();
                }
            }
        }
    }
}