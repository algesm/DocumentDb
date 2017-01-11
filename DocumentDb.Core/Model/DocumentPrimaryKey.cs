using System;

namespace DocumentDb.Core.Model
{
    public abstract class DocumentPrimaryKey
    {
        protected DocumentPrimaryKey()
        {
            DocumentId = Guid.NewGuid();
        }

        public Guid DocumentId { get; set; }
    }
}