using Autofac;
using DocumentDb.Core.Module;

namespace DocumentDb.Core.Container
{
    public class AutofacContainer
    {
        private static AutofacContainer _this;
        private readonly IContainer _container;

        public AutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DocumentDbModule>();
            _container = builder.Build();
        }
        
        public static IContainer Instance
        {
            get
            {
                if (_this == null)
                    _this = new AutofacContainer();
                return _this._container;
            }
        }
    }
}