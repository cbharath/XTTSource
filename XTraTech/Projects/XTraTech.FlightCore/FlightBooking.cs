using System;
using XTraTech.Entities.COM;
using System.Transactions;

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
            using (var scope = new System.Transactions.TransactionScope())
            {
                purchaseOrder.Save();
                scope.Complete();
            }
            return purchaseOrder.PurchaseOrderID;
        }
    }
}
