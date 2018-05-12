using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SingleSignOn.SampleHost.Controllers
{
    public class StandardController : Controller
    {
        // GET: Standard
        public ActionResult Index()
        {
            return View();
        }
    }
}