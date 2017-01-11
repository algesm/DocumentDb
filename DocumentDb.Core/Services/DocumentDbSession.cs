using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DocumentDb.Core.Context;
using DocumentDb.Core.Indexes;
using DocumentDb.Core.Model;
using DocumentDb.Core.Reflection;
using DocumentDb.Core.Serialization;

namespace DocumentDb.Core.Services
{
    internal class DocumentDbSession : IDocumentDbSession
    {
        private readonly Type _indexDesctriptor;
        private readonly IDocumentSerializer _documentSerializer;
        private readonly DocumentDbContext _context;

        public DocumentDbSession(Type indexDesctriptor, IDocumentSerializer documentSerializer)
        {
            _indexDesctriptor = indexDesctriptor;
            _documentSerializer = documentSerializer;
            _context = new DocumentDbContext();
        }

        public void Update(object sourceObj)
        {
            var key = sourceObj as DocumentPrimaryKey;

            if (key != null)
            {
                var doc = Get(key.DocumentId);
                if (doc == null)
                    throw new KeyNotFoundException($"По ключу {key.DocumentId} документ не найден");
                _documentSerializer.Serialize(sourceObj, ref doc);
            }
        }

        public void Delete(object sourceObj)
        {
            var key = sourceObj as DocumentPrimaryKey;

            if (key != null)
            {
                var doc = Get(key.DocumentId);
                if (doc == null)
                    throw new KeyNotFoundException($"По ключу {key.DocumentId} документ не найден");
                _context.Document.Remove(doc);
                return;
            }

            throw new ArgumentOutOfRangeException(String.Format("Невозможно удалить объект, так как его тип не распознан контекстом"));
        }

        public void Add(object sourceObj)
        {
            var key = sourceObj as DocumentPrimaryKey;

            if (key != null)
            {
                var doc  = new Document();

                _documentSerializer.Serialize(sourceObj, ref doc);
                doc.DocumentId = key.DocumentId;
                _context.Document.Add(doc);
   
                //Ищем индексы для заполнения
                var indexDwescriptorTypes = _indexDesctriptor.GetTypesWithGenericDefinition(sourceObj.GetType());
                var indexes = indexDwescriptorTypes.Select(indexDescriptorType => ((dynamic) Activator.CreateInstance(indexDescriptorType)).Create((dynamic)sourceObj, doc));
                
                //Каждый найденный индекс документа добавляем в контекс на сохранение
                foreach (var index in indexes)
                {
                    _context.Set(index.GetType()).Add(index);
                }
                return;
            }

            throw new ArgumentOutOfRangeException(String.Format("Невозможно сохранить объект, так как его тип не распознан контекстом"));
        }

        public IEnumerable<T> LoadWithIndexes<T, TI>(Expression<Func<TI, bool>> predicate)
            where T : DocumentPrimaryKey
            where TI : Index
        {
            var documents = RunQuery<TI>().Include(d => d.Document).Where(predicate).Select(i => i.Document);
            return As<T>(documents);
        }


        public T Get<T>(int id) where T : DocumentPrimaryKey
        {
            return As<T>(_context.Document.Find(id));
        }

        public IEnumerable<T> Load<T>() where T : DocumentPrimaryKey
        {
            var typeName = typeof(T).SimplifiedTypeName();
            var filter = RunQuery().Where(x => x.Type == typeName);

            return As<T>(filter.ToList());
        }

        public IEnumerable<T> As<T>(IEnumerable<Document> document) where T : DocumentPrimaryKey
        {
            return document.Select(As<T>);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Private Methods
        private IQueryable<Document> RunQuery()
        {
            return _context.Document;
        }

        private IQueryable<T> RunQuery<T>() where T : class
        {
            return _context.Set<T>();
        }

        private Document Get(Guid uid)
        {
            return _context.Document.Find(uid);
        }

        private T As<T>(Document document) where T : DocumentPrimaryKey
        {
            if (document == null)
            {
                return null;
            }
            var obj = _documentSerializer.Deserialize(document) as T;

            if (obj == null)
            {

                throw new ArgumentException($"Документ типа '{document.Type}' не может быть десерлизован как '{typeof(T).Name}'");
            }
            return obj;
        } 
        #endregion
    }
}