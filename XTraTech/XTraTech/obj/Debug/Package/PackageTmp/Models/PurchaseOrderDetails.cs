using System;
using System.Collections.Generic;
using System.Linq;
using XTraTech.Entities.API.Search;
using XTraTech.Entities.COM;

namespace XTraTech.Models
{
    public class PurchaseOrderDetails
    {
        public PurchaseOrder purchaseOrder = new PurchaseOrder();
        public FareXtractorRq SearchRequest = new FareXtractorRq();
        public string GetFare(List<FlightFare> flightFare, string content)
        {
            string result = string.Empty;
            if (content.ToUpper() == "BASEFARE")
            {
                decimal d = 0.0m;
                foreach (FlightFare current in flightFare)
                {
                    d += current.BaseFare * current.PaxCount;
                }
                result = d.ToString();
            }
            if (content.ToUpper() == "TAX")
            {
                decimal d2 = 0.0m;
                foreach (FlightFare current2 in flightFare)
                {
                    d2 += current2.Tax * current2.PaxCount;
                }
                result = d2.ToString();
            }
            if (content.ToUpper() == "TOTAL")
            {
                decimal d3 = 0.0m;
                foreach (FlightFare current3 in flightFare)
                {
                    d3 += (current3.BaseFare + current3.Tax) * current3.PaxCount;
                }
                result = d3.ToString();
            }
            if (content.ToUpper() == "CURRENCY")
            {
                result = flightFare.FirstOrDefault<FlightFare>().Currency;
            }
            return result;
        }
    }
}