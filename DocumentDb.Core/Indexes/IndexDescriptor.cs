using System;
using DocumentDb.Core.Model;

namespace DocumentDb.Core.Indexes
{
    /// <summary>
    /// Шаблон для иницилизации индексов данными
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIndex"></typeparam>
    public abstract class IndexDescriptor<T, TIndex> where TIndex : Index, new()
    {
        /// <summary>
        /// Создает и инициализирует индекс указанный индекс 
        /// </summary>
        /// <param name="t">Шаблон основного типа данных</param>
        /// <param name="doc">Серлизованный документ</param>
        /// <returns></returns>
        public TIndex Create(T t, Document doc)
        {
            TIndex index = new TIndex();
            Describe.Invoke(t, index);
            index.Document = doc;
            return index;
        }

        /// <summary>
        /// Действие по инициализации данных индекса, если не установить то данные в индексе будут пустыми
        /// </summary>
        protected abstract Action<T, TIndex> Describe { get; }
    }
}