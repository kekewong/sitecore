using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

namespace SingleSignOn.SampleHost.Configs.Windsor
{
    public class WindsorCompositionRoot : IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;

        public WindsorCompositionRoot(IWindsorContainer container)
        {
            this._container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var scope = _container.BeginScope();
            var controller = (IHttpController)this._container.Resolve(controllerType);

            var filters = controllerDescriptor.GetFilters();

            foreach (IFilter actionFilter in filters)
            {
                _container.Kernel.InjectProperties(actionFilter);
            }

            request.RegisterForDispose(
                new Release(
                    () =>
                    {
                        this._container.Release(controller);
                        scope.Dispose();
                    }));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                this._release = release;
            }

            public void Dispose()
            {
                this._release();
            }
        }
    }
}