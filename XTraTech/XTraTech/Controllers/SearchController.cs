using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using XTraTech.Models;
using XTraTech.Entities.COM;
using XTraTech.Entities.API.Search;
using XTraTech.FlightCore;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using XTraTech.Helper;
using System.Text;

namespace XTraTech.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            if (Session["LoggedInUser"] != null)
            {
                SearchModel model = new SearchModel();
                ViewBag.EnableNewDesign = false;
                return View(model);
            }
            else
            {
                return base.RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Index(SearchModel model)
        {
            FareXtractorRq fareXtractorRq = new FareXtractorRq();
            FareXtractorRs value = new FareXtractorRs();
            FlightSearch flightSearch = new FlightSearch();
            ActionResult result;
            try
            {
                if (Session["LoggedInUser"] != null)
                {
                    if (model.JourneyType.Equals("OneWay"))
                    {
                        OriginDestinationInformation originDestinationInformation = new OriginDestinationInformation();
                        originDestinationInformation.Origin = model.CityPares[0].Origin.Substring(1, 3);
                        originDestinationInformation.Destination = model.CityPares[0].Destination.Substring(1, 3);
                        originDestinationInformation.DepartureDateTime = model.CityPares[0].DepartureDateTime;
                        fareXtractorRq.FlightSearchDetails.AirTravelDetails.Add(originDestinationInformation);
                        fareXtractorRq.FlightSearchDetails.AirTravelType = "OneWay";
                    }
                    else
                    {
                        if (model.JourneyType.Equals("Return"))
                        {
                            OriginDestinationInformation originDestinationInformation2 = new OriginDestinationInformation();
                            originDestinationInformation2.Origin = model.CityPares[0].Origin.Substring(1, 3);
                            originDestinationInformation2.Destination = model.CityPares[0].Destination.Substring(1, 3);
                            originDestinationInformation2.DepartureDateTime = model.CityPares[0].DepartureDateTime;
                            fareXtractorRq.FlightSearchDetails.AirTravelDetails.Add(originDestinationInformation2);
                            OriginDestinationInformation originDestinationInformation3 = new OriginDestinationInformation();
                            originDestinationInformation3.Origin = model.CityPares[0].Destination.Substring(1, 3);
                            originDestinationInformation3.Destination = model.CityPares[0].Origin.Substring(1, 3);
                            originDestinationInformation3.DepartureDateTime = model.CityPares[0].ReturnDateTime;
                            fareXtractorRq.FlightSearchDetails.AirTravelDetails.Add(originDestinationInformation3);
                            fareXtractorRq.FlightSearchDetails.AirTravelType = "Return";
                        }
                        else
                        {
                            if (model.CityPares[1].Origin != null && model.CityPares[1].Destination != null && !model.CityPares[1].DepartureDateTime.ToString().Equals("01-01-0001 00:00:00"))
                            {
                                OriginDestinationInformation originDestinationInformation4 = new OriginDestinationInformation();
                                originDestinationInformation4.Origin = model.CityPares[1].Origin.Substring(1, 3);
                                originDestinationInformation4.Destination = model.CityPares[1].Destination.Substring(1, 3);
                                originDestinationInformation4.DepartureDateTime = model.CityPares[1].DepartureDateTime;
                                fareXtractorRq.FlightSearchDetails.AirTravelDetails.Add(originDestinationInformation4);
                            }
                            if (model.CityPares[2].Origin != null && model.CityPares[2].Destination != null && !model.CityPares[2].DepartureDateTime.ToString().Equals("01-01-0001 00:00:00"))
                            {
                                OriginDestinationInformation originDestinationInformation5 = new OriginDestinationInformation();
                                originDestinationInformation5.Origin = model.CityPares[2].Origin.Substring(1, 3);
                                originDestinationInformation5.Destination = model.CityPares[2].Destination.Substring(1, 3);
                                originDestinationInformation5.DepartureDateTime = model.CityPares[2].DepartureDateTime;
                                fareXtractorRq.FlightSearchDetails.AirTravelDetails.Add(originDestinationInformation5);
                            }
                            if (model.CityPares[3].Origin != null && model.CityPares[3].Destination != null && !model.CityPares[3].DepartureDateTime.ToString().Equals("01-01-0001 00:00:00"))
                            {
                                OriginDestinationInformation originDestinationInformation6 = new OriginDestinationInformation();
                                originDestinationInformation6.Origin = model.CityPares[3].Origin.Substring(1, 3);
                                originDestinationInformation6.Destination = model.CityPares[3].Destination.Substring(1, 3);
                                originDestinationInformation6.DepartureDateTime = model.CityPares[3].DepartureDateTime;
                                fareXtractorRq.FlightSearchDetails.AirTravelDetails.Add(originDestinationInformation6);
                            }
                            if (model.CityPares[0].Origin != null && model.CityPares[0].Destination != null && !model.CityPares[0].DepartureDateTime.ToString().Equals("01-01-0001 00:00:00"))
                            {
                                OriginDestinationInformation originDestinationInformation7 = new OriginDestinationInformation();
                                originDestinationInformation7.Origin = model.CityPares[0].Origin.Substring(1, 3);
                                originDestinationInformation7.Destination = model.CityPares[0].Destination.Substring(1, 3);
                                originDestinationInformation7.DepartureDateTime = model.CityPares[0].DepartureDateTime;
                                fareXtractorRq.FlightSearchDetails.AirTravelDetails.Add(originDestinationInformation7);
                            }
                            fareXtractorRq.FlightSearchDetails.AirTravelType = "OpenJaw";
                        }
                    }
                    XTraTech.Entities.API.Search.Passenger passenger = new XTraTech.Entities.API.Search.Passenger();
                    passenger.Code = "ADT";
                    passenger.Quantity = model.AdultCount.ToString();
                    fareXtractorRq.FlightSearchDetails.AirTravelers.Add(passenger);
                    if (model.ChildCount > 0)
                    {
                        XTraTech.Entities.API.Search.Passenger passenger2 = new XTraTech.Entities.API.Search.Passenger();
                        passenger2.Code = "CHD";
                        passenger2.Quantity = model.ChildCount.ToString();
                        fareXtractorRq.FlightSearchDetails.AirTravelers.Add(passenger2);
                    }
                    if (model.InfantCount > 0)
                    {
                        XTraTech.Entities.API.Search.Passenger passenger3 = new XTraTech.Entities.API.Search.Passenger();
                        passenger3.Code = "INF";
                        passenger3.Quantity = model.InfantCount.ToString();
                        fareXtractorRq.FlightSearchDetails.AirTravelers.Add(passenger3);
                    }
                    fareXtractorRq.FlightSearchDetails.AirTravelPreference.CabinClass = model.CabinClass;
                    fareXtractorRq.Version = "1.0.0";
                    flightSearch.GetFlightResults(fareXtractorRq);
                    value = flightSearch.rs;
                    base.Session["SearchResults"] = value;
                    base.Session["SearchRequest"] = fareXtractorRq;
                    result = base.RedirectToAction("SearchResponse", "Search");
                }
                else
                {
                    return base.RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                string text = string.Empty;
                if (ex.InnerException != null)
                {
                    text = ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
                }
                result = base.RedirectToActionPermanent("Index", "Error", new
                {
                    ErrorMsg = string.Concat(new string[]
					{
						ex.Message,
						"\n",
						ex.StackTrace,
						"\n\n",
						text
					})
                });
            }
            return result;
        }

        public ActionResult SearchResponse()
        {
            SearchResponse searchResponse = new SearchResponse();
            FareXtractorRs fareXtractorRs = new FareXtractorRs();
            List<XTraTech.Entities.COM.FlightDetail> list = new List<XTraTech.Entities.COM.FlightDetail>();
            try
            {
                if (Session["LoggedInUser"] != null)
                {
                    if (base.Session["SearchResults"] != null)
                    {
                        fareXtractorRs = (base.Session["SearchResults"] as FareXtractorRs);
                        foreach (AirItinerary current in fareXtractorRs.Itineraries)
                        {
                            list.Add(searchResponse.ConvertFromAirItinerarytoFlightDetails(current));
                        }
                        base.Session["SesrchID"] = fareXtractorRs.SearchID;
                        searchResponse.SearchResults = list;
                    }
                    if (base.Session["SearchRequest"] != null)
                    {
                        searchResponse.SearchRequest = (base.Session["SearchRequest"] as FareXtractorRq);
                    }
                    ViewBag.PageName = "Search";
                }
                else
                {
                    return base.RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                string text = string.Empty;
                if (ex.InnerException != null)
                {
                    text = ex.InnerException.Message + "\n" + ex.InnerException.StackTrace;
                }
                return base.RedirectToActionPermanent("Index", "Error", new
                {
                    ErrorMsg = string.Concat(new string[]
					{
						ex.Message,
						"\n",
						ex.StackTrace,
						"\n\n",
						text
					})
                });
            }
            return base.View(searchResponse);
        }

        public ActionResult AirportList(string term)
        {
            List<Airport> source = new List<Airport>();
            source = (base.HttpContext.Application["Airports"] as List<Airport>);
            var data =
                from r in source
                where r.airportCode.ToLower().Contains(term.ToLower())
                select new
                {
                    r.airportName,
                    r.airportCode
                };
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DrawChart()
        {
            // Populate series with random data
            System.Web.UI.DataVisualization.Charting.Chart Chart1 = new System.Web.UI.DataVisualization.Charting.Chart();
            // Create new data series and set it's visual attributes
            // Populate series data
            // Populate series data
            double[] yValues = { 65.62, 75.54, 60.45, 34.73, 85.42 };
            string[] xValues = { "France", "Canada", "Germany", "USA", "Italy" };

            Series series = new Series("Default");
            series.Points.DataBindXY(xValues, yValues);

            // Set Doughnut chart type
            series.ChartType = SeriesChartType.Column;
            Chart1.Series.Add(series);
            //var chart = new Chart(width: 300, height: 200)
            //    .AddSeries("Booked",
            //    chartType: "Column",
            //                xValue: new[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" },
            //                yValues: new[] { "50", "10", "80", "30", "60", "20", "10", "60", "50", "20", "80", "90" })
            //                .GetBytes("png");
            //using (var ms = new MemoryStream())
            //{
            var ms = new MemoryStream();
            Chart1.SaveImage(ms, ChartImageFormat.Png);
            return File(ms.ToArray(), "image/png");
            //}
        }


        public void Send(string ItineraryID)
        {
            PassengerModel passengerModel = new PassengerModel();
            FareXtractorRs fareXtractorRs = new FareXtractorRs();
            SearchResponse searchResponse = new SearchResponse();
            List<XTraTech.Entities.COM.FlightDetail> list = new List<XTraTech.Entities.COM.FlightDetail>();
            if (base.Session["SearchRequest"] != null)
            {
                passengerModel.SearchRequest = (base.Session["SearchRequest"] as FareXtractorRq);
            }
            if (base.Session["SearchResults"] != null)
            {
                
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
            }

            string str = RenderPartialViewToString(this, "~/Views/Shared/_EmailItinerary.cshtml", list.FirstOrDefault());
            //Email Here.
            Email email = new Email();
            email.emailTo = "chumbalekar.bharath@gmail.com";
            email.RunInBackground = true;
            email.subject = "Account Registration Success";

            StringBuilder sb = new StringBuilder();
            sb.Append(str);

            email.body = sb.ToString();
            email.Send();
        }
        public string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }
    }
}
