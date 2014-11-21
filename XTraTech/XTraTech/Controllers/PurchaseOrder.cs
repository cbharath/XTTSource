using System;
using System.Web.Mvc;
using XTraTech.Entities.API.Search;
using XTraTech.Entities.COM;
using XTraTech.Helper;
using XTraTech.Models;

namespace XTraTech.Controllers
{
    public class PurchaseOrderController : Controller
    {
        public ActionResult Index(string BookingId)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            PurchaseOrderDetails purchaseOrderDetails = new PurchaseOrderDetails();
            string purchaseOrderRef = string.Empty;
            if (!string.IsNullOrEmpty(BookingId))
            {
                purchaseOrder.PurchaseOrderID = XTraTech.Helper.Helper.GetPurchaseOrderID(BookingId).ToString();
                purchaseOrder.Load(true, true);
            }
            else
            {
                if (base.Session["BookingResponse"] != null)
                {
                    purchaseOrderRef = base.Session["BookingResponse"].ToString();
                    purchaseOrder.PurchaseOrderID = XTraTech.Helper.Helper.GetPurchaseOrderID(purchaseOrderRef).ToString();
                    purchaseOrder.Load(true, true);
                }
            }
            if (base.Session["SearchRequest"] != null)
            {
                purchaseOrderDetails.SearchRequest = (base.Session["SearchRequest"] as FareXtractorRq);
            }
            purchaseOrderDetails.purchaseOrder = purchaseOrder;
            return base.View(purchaseOrderDetails);
        }
    }
}