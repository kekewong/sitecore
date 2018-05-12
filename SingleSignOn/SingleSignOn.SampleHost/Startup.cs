using Castle.Windsor;

using SingleSignOn.SampleHost;

namespace SingleSignOn.SampleHost
{
    public partial class Startup
    {
        private static IWindsorContainer _container;

        public static void SetContainer(IWindsorContainer container)
        {
            _container = container;
        }

        //public void Configuration(IAppBuilder app)
        //{
        //    ConfigureAuth(app);
        //}
    }
}