using System;
using XTraTech.Entities.COM;

namespace XTraTech.FlightCore
{
    public class FlightBooking
    {
        public string DoBooking(PurchaseOrder purchaseOrder)
        {
            string purchaseOrderID = string.Empty;
            foreach (FlightDetail item in purchaseOrder.FlightDetails)
            {
                item.TktTimeLimit = DateTime.Now.AddDays(1.0);
            }
            purchaseOrder.Save();
            return purchaseOrder.PurchaseOrderID;
        }
    }
}
