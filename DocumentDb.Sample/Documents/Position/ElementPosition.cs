namespace DocumentDb.Sample.Documents.Position
{
    /// <summary>
    /// Представляет позицию элемента.
    /// </summary>

    public class ElementPosition
    {
        /// <summary>
        /// Получить или установить идентификатор записи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получить или установить идентификатор элемента.
        /// </summary>
        public int ElementId { get; set; }

        /// <summary>
        /// Получить или установить позицию.
        /// </summary>
        public Position Position { get; set; }
    }
}