using System;

namespace DocumentDb.Sample.Documents.Position
{
    /// <summary>
    /// Представляет позицию элемента в периоде.
    /// </summary>
    public class CaseElementPosition
    {
        /// <summary>
        /// Получить или установить идентификатор записи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получить или установить идентификатор периода.
        /// </summary>
        public int CaseId { get; set; }

        /// <summary>
        /// Получить или установить позицию элемента.
        /// </summary>
        public ElementPosition ElementPosition { get; set; }
    }
}