namespace DocumentDb.Sample.Documents.Position
{
    /// <summary>
    /// Представляет позицию.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Получить или установить идентификатор записи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получить или установить горизонтальную координату.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Получить ии установить вертикальную координату.
        /// </summary>
        public double Y { get; set; }
    }
}