using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XTraTech.Entities.API.Search;
using XTraTech.Models;
using XTraTechWeb.Models;

namespace XTraTechWeb.Controllers
{
    public class PassengerDetailsController : Controller
    {
        // GET: PassengerDetails
        public ActionResult Index(string ItineraryID)
        {
            PassengerModel passengerModel = new PassengerModel();
            FareXtractorRs fareXtractorRs = new FareXtractorRs();
            SearchResponse searchResponse = new SearchResponse();
            if (Session["LoggedInUser"] != null)
            {
                if (base.Session["SearchRequest"] != null)
                {
                    passengerModel.SearchRequest = (base.Session["SearchRequest"] as FareXtractorRq);
                }
                if (base.Session["SearchResults"] != null)
                {
                    List<XTraTech.Entities.COM.FlightDetail> list = new List<XTraTech.Entities.COM.FlightDetail>();
                    if (base.Session["SearchResults"] != null)
                    {
                        fareXtractorRs = (base.Session["SearchResults"] as FareXtractorRs);
                        foreach (AirItinerary current in fareXtractorRs.Itineraries)
                        {
                            list.Add(searchResponse.ConvertFromAirItinerarytoFlightDetails(current));
                        }
                    }
                    list = (
                        from fd in list
                        where fd.ItineraryID == ItineraryID
                        select fd).ToList<XTraTech.Entities.COM.FlightDetail>();
                    passengerModel.SelectedOptions = list;
                    base.Session["SelectedOptions"] = list;
                }
                ViewBag.PageName = "PassengerDetails";
                //Adding sleep for 10 sec.

                System.Threading.Thread.Sleep(5000);
                return base.View(passengerModel);
            }
            else
            {
                return base.RedirectToAction("Index", "Login");
            }
        }
    }
}