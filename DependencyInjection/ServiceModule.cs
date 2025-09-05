using Autofac;
using RRMS.Services;

namespace RRMS.DependencyInjection
{
    public class ServiceModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder) 
        {
            builder.RegisterType<ChatMessageService>().As<IChatMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<EmergencyContactService>().As<IEmergencyContactService>().InstancePerLifetimeScope();
            builder.RegisterType<RoomService>().As<IRoomService>().InstancePerLifetimeScope();
            builder.RegisterType<UserAccountService>().As<IUserAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<ViewService>().As<IViewService>().InstancePerLifetimeScope();
            builder.RegisterType<WebTokenService>().As<IWebTokenService>().InstancePerLifetimeScope();
        }
    }
}
