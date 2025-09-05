using Autofac;
using RRMS.Repositories;

namespace RRMS.DependencyInjection
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChatMessageRepository>().As<IChatMessageRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EmergencyContactRepository>().As<IEmergencyContactRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoomRepository>().As<IRoomRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserAccountRepository>().As<IUserAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ViewRepository>().As<IViewRepository>().InstancePerLifetimeScope();
        }
    }
}
