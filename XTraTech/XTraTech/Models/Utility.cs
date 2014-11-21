using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XTraTech.Entities.COM;

namespace XTraTech.Models
{
    public class Utility
    {
        public static string GetAirportName(string airportCode)
        {
            string result = string.Empty;
            List<Airport> source = new List<Airport>();
            source = (HttpContext.Current.Application["Airports"] as List<Airport>);
            IEnumerable<Airport> source2 =
                from r in source
                where r.airportCode.ToLower().Equals(airportCode.ToLower())
                select r;
            if (source2.Any<Airport>())
            {
                result = source2.FirstOrDefault<Airport>().airportName;
            }
            return result;
        }
        public static string GetCabinName(string CabinCode)
        {
            string result = string.Empty;
            string a;
            if ((a = CabinCode.ToUpper()) != null && a == "Y")
            {
                result = "Economy";
            }
            else
            {
                result = "Economy";
            }
            return result;
        }
        public static string MinutesToHours(string minues)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(minues))
            {
                TimeSpan timeSpan = TimeSpan.FromMinutes(Convert.ToDouble(minues));
                result = string.Format("{0}h {1}m", timeSpan.Hours, timeSpan.Minutes);
            }
            return result;
        }
    }
}