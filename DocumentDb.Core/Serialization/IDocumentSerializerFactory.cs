namespace DocumentDb.Core.Serialization
{
    public interface IDocumentSerializerFactory
    {
        IDocumentSerializer Create(DocumentSerializer serializer);
    }
}