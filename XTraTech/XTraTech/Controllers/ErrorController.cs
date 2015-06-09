using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XTraTech.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index(string ErrorMsg)
        {
            ViewBag.ErrorMsg = ErrorMsg;
            return View();
        }

    }
}
