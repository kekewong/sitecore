using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SingleSignOn.SampleHost.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index(bool showSuccessCreateUser = false)
        {
            ViewBag.SuccessUserCreate = showSuccessCreateUser;
            return View();
        }
    }
}