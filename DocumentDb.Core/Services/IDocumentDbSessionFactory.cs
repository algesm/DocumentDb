using System;
using DocumentDb.Core.Indexes;
using DocumentDb.Core.Serialization;

namespace DocumentDb.Core.Services
{
    public interface IDocumentDbSessionFactory : IDisposable
    {
        /// <summary>
        /// Создает новый экземпляр <see cref="IDocumentDbSession"/>
        /// </summary>
        IDocumentDbSession CreateSession();
    }
}