using System;
using DocumentDb.Core.Model;

namespace DocumentDb.Core.Indexes
{
    /// <summary>
    /// Содержит списко полей для определения Индекса
    /// </summary>
    public interface IIndex
    {
        Guid IndexId { get; set; }
        Document Document { get; set; }
    }
}