using System.Collections.Generic;
using System.Web.Http.Filters;
using Castle.MicroKernel;

namespace SingleSignOn.Configs.Windsor
{
    public class WindsorFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        readonly IKernel _kernel;

        public WindsorFilterProvider(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public new IEnumerable<FilterInfo> GetFilters(System.Web.Http.HttpConfiguration configuration, System.Web.Http.Controllers.HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);

            foreach (var actionFilter in filters)
            {
                _kernel.InjectProperties(actionFilter);
            }

            return filters;
        }
    }
}