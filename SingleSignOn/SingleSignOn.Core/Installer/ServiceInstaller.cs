using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SingleSignOn.Core.Services;

namespace SingleSignOn.Core.Installer
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAuthCacheService>()
                .ImplementedBy<AuthCacheService>()
                .DependsOn(
                        Dependency.OnValue("prefixKey", "AuthCache"))
                .LifestyleSingleton());

            container.Register(Component.For<IUserQueryCacheService>()
                .ImplementedBy<UserQueryCacheService>()
                .DependsOn(
                    Dependency.OnValue("prefixKey", "UserQueryCache"))
                .LifestyleSingleton());

            container.Register(Component.For<IDatabaseConnection>()
                .ImplementedBy<DatabaseConnection>()
                .DependsOn(
                    Dependency.OnValue("connectionString", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                .LifestyleTransient());

            container.Register(Component.For<ISimplePasswordHash>()
                .ImplementedBy<SimplePasswordHash>()
                .LifestyleTransient());

            container.Register(Component.For<IUserRepository>()
                .ImplementedBy<UserRepository>()
                .LifestyleTransient());

        }
    }
}
