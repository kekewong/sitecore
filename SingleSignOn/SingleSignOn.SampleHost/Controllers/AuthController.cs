using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using SingleSignOn.Core.Domain;
using SingleSignOn.Core.Mediators.Messages;
using SingleSignOn.SampleHost.Models;

namespace SingleSignOn.SampleHost.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            //if (CurrentUser != null)
            //{
            //    var baseUrl = System.Configuration.ConfigurationManager.AppSettings["SingleSignOnAuthServicePath"];
            //    var uri = baseUrl + "/CheckUserSession?username=" + CurrentUser.Username;
            //    var httpClient = new HttpClient();
            //    var response = httpClient.GetAsync(uri).Result;
            //    if (!response.IsSuccessStatusCode)
            //    {
            //        CurrentUser = null;
            //        IsSignedInByCached = false;
            //        return View();
            //    }

            //    if (CurrentUser.Role == UserRole.Admin) return RedirectToAction("Index", "Admin");
            //    if (CurrentUser.Role == UserRole.Standard) return RedirectToAction("Index", "Standard");

            //}
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginModel model)
        {
            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["SingleSignOnAuthServicePath"];
            var uri = baseUrl + "/Login";
            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(uri, stringContent).Result;
            var message = JsonConvert.DeserializeObject<SignInResultMessage>(response.Content.ReadAsStringAsync().Result);

            if (response.IsSuccessStatusCode)
            {
                CurrentUser = message.User;
                IsSignedInByCached = message.IsSignInByCached;
                if (CurrentUser.Role == UserRole.Admin) return RedirectToAction("Index", "Admin");
                if (CurrentUser.Role == UserRole.Standard) return RedirectToAction("Index", "Standard");
            }

            ModelState.AddModelError("Error", "Invalid credentials");
            return View("Login");
        }

        public ActionResult CreateUser(CreateUserMessage message)
        {
            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["SingleSignOnUsersServicePath"];
            var uri = baseUrl;
            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(uri, stringContent).Result;
            var resp = JsonConvert.DeserializeObject<CreateUserResultMessage>(response.Content.ReadAsStringAsync().Result);
            if (resp.Success)
            {
                return RedirectToAction("Index", "Admin", new { showSuccessCreateUser = true });
            }
            ModelState.AddModelError("Error", string.Join("<br/>", resp.ValidationResult.Errors.Select(x => x.ErrorMessage)));
            return View("~/Views/Admin/Index.cshtml");
        }
    }
}