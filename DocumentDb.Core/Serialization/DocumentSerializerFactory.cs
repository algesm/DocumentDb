using Autofac;

namespace DocumentDb.Core.Serialization
{
    public class DocumentSerializerFactory : IDocumentSerializerFactory
    {
        private readonly IComponentContext _container;

        public DocumentSerializerFactory(IComponentContext container)
        {
            _container = container;
        }

        public IDocumentSerializer Create(DocumentSerializer serializer)
        {
            return _container.ResolveKeyed<IDocumentSerializer>(serializer);
        }
    }
}