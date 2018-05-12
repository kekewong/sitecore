using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace SingleSignOn.SampleHost.Configs.Windsor
{
    internal class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public WindsorDependencyResolver(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(this._kernel);
        }

        public object GetService(Type serviceType)
        {
            return this._kernel.HasComponent(serviceType) ? this._kernel.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._kernel.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose() { }
    }

    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IKernel _kernel;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IKernel kernel)
        {
            this._kernel = kernel;
            this._scope = kernel.BeginScope();
        }

        public object GetService(Type serviceType)
        {
            return this._kernel.HasComponent(serviceType) ? this._kernel.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._kernel.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            this._scope.Dispose();
        }
    }
}