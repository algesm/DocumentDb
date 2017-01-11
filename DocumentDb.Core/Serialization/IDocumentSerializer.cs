using DocumentDb.Core.Model;

namespace DocumentDb.Core.Serialization
{
    /// <summary>
    /// Сериализация и десерлизация документа в JSON
    /// </summary>
    public interface IDocumentSerializer
    {
        /// <summary>
        /// Указывает, какая библиотека используется в имплементации
        /// </summary>
        DocumentSerializer SerializerType { get; }

        /// <summary>
        /// Серлизует документ в JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="doc"></param>
        void Serialize(object obj, ref Document doc);

        /// <summary>
        /// Десерлизует документ ищ JSON
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        object Deserialize(Document doc);
    }
}