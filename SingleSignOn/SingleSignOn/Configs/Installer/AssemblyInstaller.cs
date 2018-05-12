using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SingleSignOn.Configs.Installer
{
    public class AssemblyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assemblies = new List<Assembly>
            {
                Assembly.GetExecutingAssembly()
            };

            var relatedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.Name.StartsWith("SingleSignOn"));
            assemblies.AddRange(relatedAssemblies.Select(Assembly.Load));
        }
    }
}