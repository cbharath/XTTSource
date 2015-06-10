using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XTraTech.Entities.API.Search;
using XTraTech.Entities.COM;
using XTraTechWeb.Models;

namespace XTraTechWeb.Controllers
{
    public class PurchaseOrderController : Controller
    {
        // GET: PurchaseOrder
        public ActionResult Index(string BookingId)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            PurchaseOrderDetails purchaseOrderDetails = new PurchaseOrderDetails();
            string purchaseOrderRef = string.Empty;
            if (Session["LoggedInUser"] != null)
            {
                if (!string.IsNullOrEmpty(BookingId))
                {
                    purchaseOrder.PurchaseOrderID = XTraTech.Helper.Helper.GetPurchaseOrderID(BookingId).ToString();
                    purchaseOrder.Load(true, true);
                    purchaseOrderDetails.IsShowThankYou = false;
                }
                else
                {
                    if (base.Session["BookingResponse"] != null)
                    {
                        purchaseOrderRef = base.Session["BookingResponse"].ToString();
                        purchaseOrder.PurchaseOrderID = XTraTech.Helper.Helper.GetPurchaseOrderID(purchaseOrderRef).ToString();
                        purchaseOrder.Load(true, true);
                        purchaseOrderDetails.IsShowThankYou = true;
                    }
                }
                if (base.Session["SearchRequest"] != null)
                {
                    purchaseOrderDetails.SearchRequest = (base.Session["SearchRequest"] as FareXtractorRq);
                }
                purchaseOrderDetails.purchaseOrder = purchaseOrder;
                return base.View(purchaseOrderDetails);
            }
            else
            {
                return base.RedirectToAction("Index", "Login");
            }
        }
    }
}