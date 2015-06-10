using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XTraTech.Entities.COM;
using XTraTechWeb.Models;

namespace XTraTechWeb.Controllers
{
    public class ReservationsController : Controller
    {
        //
        // GET: /Reservations/

        public ActionResult Index()
        {
            if (Session["LoggedInUser"] != null)
            {
                ReservationsModel res = new ReservationsModel();
                PurchaseOrder purchaseorder = new PurchaseOrder();
                res.Reservations = purchaseorder.LoadAll();
                return View(res);
            }
            else
            {
                return base.RedirectToAction("Index", "Login");
            }
        }
    }
}