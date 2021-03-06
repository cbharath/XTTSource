﻿using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using XTraTech.Entities.API.Search;
using XTraTech.Entities.COM;
using XTraTech.Models;
using XTraTech.FlightCore;

namespace XTraTech.Controllers
{
    public class PassengerDetailsController : Controller
    {
        //
        // GET: /PassengerDetails/

        public ActionResult Index(string ItineraryID)
        {
            PassengerModel passengerModel = new PassengerModel();
			FareXtractorRs fareXtractorRs = new FareXtractorRs();
			SearchResponse searchResponse = new SearchResponse();
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
			return base.View(passengerModel);
        }

        [HttpPost]
        public ActionResult Index(PassengerModel model)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            try
            {
                if (base.Session["SesrchID"] != null)
                {
                    purchaseOrder.SearchID = base.Session["SesrchID"].ToString();
                }
                List<PassengerDetail> list = new List<PassengerDetail>();
                if (model.Passengers.Count > 0)
                {
                    foreach (XTraTech.Models.Passenger current in model.Passengers)
                    {
                        PassengerDetail passengerDetail = new PassengerDetail();
                        passengerDetail.Address1 = "";
                        passengerDetail.Address2 = "";
                        passengerDetail.AreaCode = model.MobileNum;
                        passengerDetail.CountryCode = current.Country;
                        if (!string.IsNullOrEmpty(current.Day) && !string.IsNullOrEmpty(current.Month) && !string.IsNullOrEmpty(current.Year))
                        {
                            DateTime value = new DateTime(Convert.ToInt32(current.Year), Convert.ToInt32(current.Month), Convert.ToInt32(current.Day));
                            passengerDetail.DOB = new DateTime?(value);
                            if (DateTime.Now.Year - Convert.ToInt32(current.Year) >= 18)
                            {
                                passengerDetail.PaxType = XTraTech.Entities.COM.Enumaration.PaxType.ADT;
                            }
                            else
                            {
                                if (DateTime.Now.Year - Convert.ToInt32(current.Year) <= 12 && DateTime.Now.Year - Convert.ToInt32(current.Year) > 2)
                                {
                                    passengerDetail.PaxType = XTraTech.Entities.COM.Enumaration.PaxType.CHD;
                                }
                                else
                                {
                                    passengerDetail.PaxType = XTraTech.Entities.COM.Enumaration.PaxType.INF;
                                }
                            }
                        }
                        passengerDetail.Email = model.Email;
                        passengerDetail.FFNumber = current.FFNum;
                        passengerDetail.FirstName = current.FirstName;
                        passengerDetail.Gender = current.Gender;
                        passengerDetail.LastName = current.LastName;
                        passengerDetail.MealPref = current.MealReq;
                        passengerDetail.Mobile = model.MobileNum;
                        if (!string.IsNullOrEmpty(current.PassportNum))
                        {
                            DateTime passportDOE = new DateTime(Convert.ToInt32(current.PYear), Convert.ToInt32(current.PMonth), Convert.ToInt32(current.PDay));
                            passengerDetail.PassportDOE = passportDOE;
                            passengerDetail.PassportNationality = current.Nationality;
                            passengerDetail.PassportNumber = current.PassportNum;
                        }
                        passengerDetail.SeatPref = current.SeatPref;
                        passengerDetail.Title = current.Title;
                        list.Add(passengerDetail);
                    }
                    base.Session["Passengers"] = list;
                }
                purchaseOrder.PassengerDetails = list;
                if (base.Session["SelectedOptions"] != null)
                {
                    purchaseOrder.FlightDetails = (base.Session["SelectedOptions"] as List<XTraTech.Entities.COM.FlightDetail>);
                }
                FlightBooking flightBooking = new FlightBooking();
                string value2 = flightBooking.DoBooking(purchaseOrder);
                base.Session["BookingResponse"] = value2;
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
            return base.RedirectToAction("Index", "PurchaseOrder");
        }
        public ActionResult SearchResponse(string ItineraryID)
        {
            return base.RedirectToActionPermanent("SearchResponse", "Search");
        }
    }
}
