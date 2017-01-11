using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DocumentDb.Core.Indexes;
using DocumentDb.Core.Model;

namespace DocumentDb.Core.Services
{
    /// <summary>
    /// Определяет соединение к SQL DocumentDb.
    /// </summary>
    public interface IDocumentDbSession : IDisposable
    {
        void Add(object sourceObj);

        void Update(object sourceObj);

        void Delete(object sourceObj);

        T Get<T>(int id) where T : DocumentPrimaryKey;

        IEnumerable<T> LoadWithIndexes<T, TI>(Expression<Func<TI, bool>> predicate)
            where T : DocumentPrimaryKey
            where TI : Index;

        IEnumerable<T> Load<T>() where T : DocumentPrimaryKey;

        IEnumerable<T> As<T>(IEnumerable<Document> document) where T : DocumentPrimaryKey;
        
        void Commit();
    }
}