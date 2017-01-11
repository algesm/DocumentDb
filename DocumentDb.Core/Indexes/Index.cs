using System;
using DocumentDb.Core.Model;

namespace DocumentDb.Core.Indexes
{
    /// <summary>
    /// Класс для реализации индекса документа
    /// </summary>
    public abstract class Index : IIndex
    {
        private Guid _indexId;

        protected Index()
        {
            _indexId = Guid.NewGuid();
        }

        public Guid IndexId
        {
            get { return _indexId; }
            set { _indexId = value; }
        }

        public virtual Document Document { get; set; }
    }
}