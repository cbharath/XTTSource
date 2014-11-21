using System;
using System.Collections.Generic;
using System.Linq;
using XTraTech.Entities.COM;
namespace XTraTech.Entities
{
    public class Helper
    {
        public static string GetFare(List<FlightFare> flightFare, string content)
        {
            string return_result = string.Empty;
            if (content.ToUpper() == "BASEFARE")
            {
                decimal totalBaseFare = 0.0m;
                foreach (FlightFare item in flightFare)
                {
                    totalBaseFare += item.BaseFare * item.PaxCount;
                }
                return_result = totalBaseFare.ToString();
            }
            if (content.ToUpper() == "TAX")
            {
                decimal totalTax = 0.0m;
                foreach (FlightFare item in flightFare)
                {
                    totalTax += item.Tax * item.PaxCount;
                }
                return_result = totalTax.ToString();
            }
            if (content.ToUpper() == "TOTAL")
            {
                decimal total = 0.0m;
                foreach (FlightFare item in flightFare)
                {
                    total += (item.BaseFare + item.Tax) * item.PaxCount;
                }
                return_result = total.ToString();
            }
            if (content.ToUpper() == "CURRENCY")
            {
                return_result = flightFare.FirstOrDefault<FlightFare>().Currency;
            }
            return return_result;
        }
    }
}
