using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XTraTech.Entities.API.Search;
using Adapters;

namespace Tickets.ID
{
    public class TicketsSearch
    {
        public void Search(FareXtractorRq rq, List<AirItinerary> itineraries)
        {
            HttpPostApi http = new HttpPostApi();
            ParseResponse parser = new ParseResponse();
            //Get Token ID
            string token = string.Empty;
            Authentication authentication = new Authentication();
            token = authentication.GetToken();
            Passenger adults = (
                from p in rq.FlightSearchDetails.AirTravelers
                where p.Code == "ADT"
                select p).FirstOrDefault<Passenger>();
            Passenger child = (
                from p in rq.FlightSearchDetails.AirTravelers
                where p.Code == "CHD"
                select p).FirstOrDefault<Passenger>();
            Passenger infant = (
                from p in rq.FlightSearchDetails.AirTravelers
                where p.Code == "INF"
                select p).FirstOrDefault<Passenger>();
            string adultcount = adults.Quantity;
            string childcount = "0";
            string infantcount = "0";
            if (child != null)
            {
                childcount = adults.Quantity;
            }
            if (infant != null)
            {
                infantcount = adults.Quantity;
            }

            string origin = rq.FlightSearchDetails.AirTravelDetails[0].Origin;
            string destination = rq.FlightSearchDetails.AirTravelDetails[0].Destination;
            string depart_date = rq.FlightSearchDetails.AirTravelDetails[0].DepartureDateTime.ToString("yyyy-MM-dd");
            string return_date = string.Empty;
            string postString = string.Empty;
            if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "RETURN")
            {
                return_date = rq.FlightSearchDetails.AirTravelDetails[1].DepartureDateTime.ToString("yyyy-MM-dd");
                postString = Configurations.URL + "search/flight?d=" + origin + "&a=" + destination + "&date=" + depart_date + "&ret_date=" + return_date + "&adult=" + adultcount + "&child=" + childcount + "&infant=" + infantcount + "&token=" + token + "&v=3" + "&output=" + Configurations.OutPut;
            }
            if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "ONEWAY")
            {
                postString = Configurations.URL + "search/flight?d=" + origin + "&a=" + destination + "&date=" + depart_date + "&adult=" + adultcount + "&child=" + childcount + "&infant=" + infantcount + "&token=" + token + "&v=3" + "&output=" + Configurations.OutPut;
            }

            string ResponseXML = http.invokeURL(postString, HttpMethod.Post, ContentType.textXml);

            parser.ParseSearse(ResponseXML, itineraries,rq);
        }
    }
}
