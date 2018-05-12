
namespace SingleSignOn.SampleHost
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        //public void ConfigureAuth(IAppBuilder app)
        //{
        //    app.UseCookieAuthentication(new CookieAuthenticationOptions
        //    {
        //        AuthenticationMode = AuthenticationMode.Active,
        //        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        //        ExpireTimeSpan = TimeSpan.FromMinutes(13),
        //        CookieManager = new ChunkingCookieManager { ChunkSize = null },
        //        LoginPath = new PathString("/Auth/Login"),
        //        SlidingExpiration = true,
        //        CookieName = "SiteCoreSingleSignOn",
        //        Provider = new CookieAuthenticationProvider
        //        {
        //            OnValidateIdentity = OnValidateIdentity(TimeSpan.FromMinutes(1)),
        //            OnException = context =>
        //            {
        //                throw context.Exception;
        //            }
        //        }
        //    });
        //}

        //public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity(TimeSpan validateInterval)
        //{
        //    return delegate (CookieValidateIdentityContext context)
        //    {
        //        List<Task> tasks = new List<Task>();
        //        var utcNow = DateTimeOffset.UtcNow;
        //        if ((context.Options != null) && (context.Options.SystemClock != null))
        //        {
        //            utcNow = context.Options.SystemClock.UtcNow;
        //        }
        //        var issuedUtc = context.Properties.IssuedUtc;
        //        var validate = !issuedUtc.HasValue;

        //        if (issuedUtc.HasValue)
        //        {
        //            var span = utcNow.Subtract(issuedUtc.Value);
        //            validate = span > validateInterval;
        //        }

        //        if (validate)
        //        {
        //            var username = context.Identity.ClaimUsername();
        //            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["SingleSignOnAuthServicePath"];
        //            var uri = baseUrl + "/CheckUserSession?username=" + username;
        //            var httpClient = new HttpClient();
        //            var response = httpClient.GetAsync(uri).Result;
        //            if (!response.IsSuccessStatusCode)
        //            {
        //                tasks.Add(Task.Factory.StartNew(() =>
        //                {
        //                    context.RejectIdentity();
        //                    context.OwinContext.Authentication.SignOut(new[] { context.Options.AuthenticationType });
        //                }));
        //            }
        //            else
        //            {
        //                var responseBody = response.Content.ReadAsStringAsync().Result;
        //                var data = Json.Decode(responseBody);
        //                var identityFactory = _container.Resolve<IClaimsIdentityFactory>();
        //                var id = identityFactory.CreateIdentity(new UserDTO
        //                {
        //                    Id = 
        //                });
        //                tasks.Add(Task.Factory.StartNew(() =>
        //                {
        //                    context.OwinContext.Authentication.SignIn(id);
        //                }));
        //            }

        //        }
        //        return Task.WhenAll(tasks.ToArray());
        //    };
        //}
    }
}