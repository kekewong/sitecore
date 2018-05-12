using System.Web.Mvc;
using SingleSignOn.Core.Domain;
using SingleSignOn.Core.DTOs;

namespace SingleSignOn.SampleHost.Controllers
{
    public class BaseController : Controller
    {
        protected static UserDTO CurrentUser { get; set; }
        protected static bool IsSignedInByCached { get; set; }

        public BaseController()
        {
            if (CurrentUser != null)
            {
                ViewBag.CurrentUser = CurrentUser;
            }
            if (IsSignedInByCached)
            {
                ViewBag.IsSignedInByCached = IsSignedInByCached;
            }
        }
    
    }
}