using Autofac;
using Project.Service.Common;

namespace Project.Service
{
    public class ServiceDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MakeService>().As<IMakeService>().InstancePerLifetimeScope();
            builder.RegisterType<ModelService>().As<IModelService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
