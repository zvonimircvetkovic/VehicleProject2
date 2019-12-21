using Autofac;
using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Repository.Common;

namespace Project.Repository
{
    public class RepositoryDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<VehicleContext>().As<IVehicleContext>().InstancePerLifetimeScope();
            builder.RegisterType<MakeRepository>().As<IMakeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ModelRepository>().As<IModelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
