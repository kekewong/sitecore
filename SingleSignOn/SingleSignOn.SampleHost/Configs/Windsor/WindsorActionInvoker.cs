using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using Castle.Windsor;

namespace SingleSignOn.SampleHost.Configs.Windsor
{
    public class WindsorActionInvoker : AsyncControllerActionInvoker
    {
        readonly IWindsorContainer _container;

        public WindsorActionInvoker(IWindsorContainer container)
        {
            _container = container;
        }

        protected override AuthorizationContext InvokeAuthorizationFilters(ControllerContext controllerContext, IList<IAuthorizationFilter> filters, ActionDescriptor actionDescriptor)
        {
            foreach (var actionFilter in filters)
            {
                _container.Kernel.InjectProperties(actionFilter);
            }

            return base.InvokeAuthorizationFilters(controllerContext, filters, actionDescriptor);
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            foreach (var actionFilter in filters)
            {
                _container.Kernel.InjectProperties(actionFilter);
            }

            return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
        }
    }
}