using System;

namespace DocumentDb.Core.Model
{
    /// <summary>
    /// Класс описывающий документ
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Описывает исходный тип документа
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Содержание документа в серлизованном виде
        /// </summary>
        public string Content { get; set; }
    }
}