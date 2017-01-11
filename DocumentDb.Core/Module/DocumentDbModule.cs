using Autofac;
using DocumentDb.Core.Reflection;
using DocumentDb.Core.Serialization;
using DocumentDb.Core.Serialization.Implementation;
using DocumentDb.Core.Services;

namespace DocumentDb.Core.Module
{
    public class DocumentDbModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentDbSettingsProvider>().As<IDocumentDbSettingsProvider>().SingleInstance();
            builder.RegisterType<DocumentDbSessionFactory>().As<IDocumentDbSessionFactory>().InstancePerDependency();
            builder.RegisterType<DocumentDbSession>().As<IDocumentDbSession>().InstancePerDependency();

            builder.RegisterType<DocumentSerializerFactory>().As<IDocumentSerializerFactory>().InstancePerDependency();

            builder.RegisterType<JsonNetDocumentSerializer>().Keyed<IDocumentSerializer>(DocumentSerializer.JsonNet).InstancePerDependency();
            builder.RegisterType<JScriptDocumentSerializer>().Keyed<IDocumentSerializer>(DocumentSerializer.JScript).InstancePerDependency();
        }
    }
}