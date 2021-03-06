﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using XTraTech.Entities.API.Search;

namespace Tickets.ID
{
    public class ParseResponse
    {
        public string ParseToken(string responseXML)
        {
            string token = string.Empty;
            try
            {
                StringReader stringReader = new StringReader(responseXML);
                XmlDocument responseDoc = new XmlDocument();
                if (!string.IsNullOrEmpty(responseXML))
                {
                    responseDoc.Load(stringReader);
                    responseDoc = XTraTech.Helper.Helper.StripDocumentNamespace(responseDoc);
                    if (responseDoc != null)
                    {
                        XmlNode nodeCommandList = responseDoc.SelectSingleNode("tiket");
                        if (nodeCommandList != null)
                        {
                            XmlNode nodeStartRouting = nodeCommandList.SelectSingleNode("token");
                            if (nodeStartRouting != null)
                            {
                                token = nodeStartRouting.InnerText;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return token;
        }

        public void ParseSearse(string responseXML, List<AirItinerary> itineraries, FareXtractorRq rq)
        {
            try
            {
                StringReader stringReader = new StringReader(responseXML);
                XmlDocument responseDoc = new XmlDocument();
                if (!string.IsNullOrEmpty(responseXML))
                {
                    responseDoc.Load(stringReader);
                    responseDoc = XTraTech.Helper.Helper.StripDocumentNamespace(responseDoc);
                    if (responseDoc != null)
                    {
                        XmlNode nodetiket = responseDoc.SelectSingleNode("tiket");
                        if (nodetiket != null)
                        {
                            XmlNode Departures = nodetiket.SelectSingleNode("departures");
                            if (Departures != null)
                            {
                                XmlNodeList DepartureResults = Departures.SelectNodes("result");


                                int secquenceNumOnword = 1000;
                                int secquenceNumReturn = 1000;
                                for (int i = 0; i < DepartureResults.Count; i++)
                                {
                                    AirItinerary airItinerary = new AirItinerary();
                                    airItinerary.SecquenceNumber = (secquenceNumOnword++).ToString();
                                    airItinerary.ItineraryID = airItinerary.SecquenceNumber + "_" + "ID";
                                    FareDetails fareDetails = new FareDetails();
                                    fareDetails.FareType = "Public";

                                    //Reading Fares here.
                                    List<XTraTech.Entities.API.Search.Fare> fares = new List<XTraTech.Entities.API.Search.Fare>();

                                    XTraTech.Entities.API.Search.Fare childfare = new XTraTech.Entities.API.Search.Fare();
                                    XTraTech.Entities.API.Search.Fare infantfare = new XTraTech.Entities.API.Search.Fare();
                                    XTraTech.Entities.API.Search.Fare adultfare = new XTraTech.Entities.API.Search.Fare();

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

                                    if (adults != null)
                                    {
                                        adultfare.PassengerType = "ADT";
                                        adultfare.PassengerQuantity = rq.FlightSearchDetails.AirTravelers[0].Quantity;
                                        adultfare.SinglePassangerBaseFare = new Money
                                        {
                                            Amount = DepartureResults[i].SelectSingleNode("price_adult").InnerText,
                                            Currency = "IDR"
                                        };

                                        Money taxes = new Money();
                                        taxes.Amount = "0.0";
                                        taxes.Currency = "IDR";
                                        adultfare.SinglePassangerTax = taxes;
                                    }

                                    if (child != null)
                                    {
                                        //child here

                                        childfare.PassengerType = "CHD";
                                        childfare.PassengerQuantity = rq.FlightSearchDetails.AirTravelers[1].Quantity;
                                        childfare.SinglePassangerBaseFare = new Money
                                        {
                                            Amount = DepartureResults[i].SelectSingleNode("price_child").InnerText,
                                            Currency = "IDR"
                                        };

                                        Money ctaxes = new Money();
                                        ctaxes.Amount = "0.0";
                                        ctaxes.Currency = "IDR";
                                        childfare.SinglePassangerTax = ctaxes;
                                    }
                                    if (infant != null)
                                    {
                                        //infant here

                                        infantfare.PassengerType = "ADT";
                                        infantfare.PassengerQuantity = rq.FlightSearchDetails.AirTravelers[2].Quantity;
                                        infantfare.SinglePassangerBaseFare = new Money
                                        {
                                            Amount = DepartureResults[i].SelectSingleNode("price_infant").InnerText,
                                            Currency = "IDR"
                                        };

                                        Money itaxes = new Money();
                                        itaxes.Amount = "0.0";
                                        itaxes.Currency = "IDR";
                                        infantfare.SinglePassangerTax = itaxes;
                                    }

                                    if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "ONEWAY")
                                    {
                                        fares.Add(adultfare);
                                        if (child != null)
                                        {
                                            fares.Add(childfare);
                                        }
                                        if (infant != null)
                                        {
                                            fares.Add(infantfare);
                                        }
                                        fareDetails.Fares = fares;
                                        airItinerary.FareDetails = fareDetails;
                                    }

                                    //Reading the Itineraries here
                                    List<FlightDetail> flightDetails = new List<FlightDetail>();
                                    FlightDetail flightDetailOnward = new FlightDetail();
                                    flightDetailOnward.JourneyType = "Onward";
                                    List<XTraTech.Entities.API.Search.FlightSegment> FlightSements = new List<XTraTech.Entities.API.Search.FlightSegment>();

                                    XmlNode flightInfos = DepartureResults[i].SelectSingleNode("flight_infos");
                                    XmlNodeList flightInfosList = flightInfos.SelectNodes("flight_info");

                                    for (int onwardLegId = 0; onwardLegId < flightInfosList.Count; onwardLegId++)
                                    {
                                        XTraTech.Entities.API.Search.FlightSegment ileg = new XTraTech.Entities.API.Search.FlightSegment();
                                        ileg.JourneyDuration = "400";//Info not exist
                                        ileg.FlightCode = flightInfosList[onwardLegId].SelectSingleNode("flight_number").InnerText.Split('-')[0];
                                        ileg.FlightNumber = flightInfosList[onwardLegId].SelectSingleNode("flight_number").InnerText.Split('-')[1];
                                        ileg.Origin = flightInfosList[onwardLegId].SelectSingleNode("departure_city").InnerText;
                                        ileg.DepartureDateTime = rq.FlightSearchDetails.AirTravelDetails[0].DepartureDateTime;
                                        ileg.MarketingAirline = ileg.FlightCode;
                                        ileg.OperatingAirline = ileg.FlightCode;
                                        ileg.Destination = flightInfosList[onwardLegId].SelectSingleNode("arrival_city").InnerText;
                                        ileg.ArrivalDateime = rq.FlightSearchDetails.AirTravelDetails[0].DepartureDateTime;
                                        ileg.CabinClass = "Y";//info not present
                                        ileg.BookingClass = "R";//info not present
                                        ileg.EquipmentType = "747-8";//info not present

                                        FlightSements.Add(ileg);
                                    }

                                    flightDetailOnward.FlightSements = FlightSements;
                                    if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "ONEWAY")
                                    {
                                        flightDetails.Add(flightDetailOnward);
                                        airItinerary.FlightDetails = flightDetails;
                                        itineraries.Add(airItinerary);
                                    }

                                    if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "RETURN")
                                    {
                                        XmlNode Returns = nodetiket.SelectSingleNode("returns");
                                        XmlNodeList ReturnResults = Returns.SelectNodes("result");

                                        if (ReturnResults != null && ReturnResults.Count > 0)
                                        {
                                            for (int j = 0; j < ReturnResults.Count; j++)
                                            {
                                                AirItinerary airItineraryReturn = new AirItinerary();
                                                airItineraryReturn.SecquenceNumber = (secquenceNumReturn++).ToString();
                                                airItineraryReturn.ItineraryID = airItineraryReturn.SecquenceNumber + "_" + "ID";
                                                FareDetails fareDetailsReturn = new FareDetails();
                                                fareDetails.FareType = "Public";

                                                //Reading Fares here.
                                                List<XTraTech.Entities.API.Search.Fare> faresReturn = new List<XTraTech.Entities.API.Search.Fare>();

                                                XTraTech.Entities.API.Search.Fare childfareReturn = new XTraTech.Entities.API.Search.Fare();
                                                XTraTech.Entities.API.Search.Fare infantfareReturn = new XTraTech.Entities.API.Search.Fare();
                                                XTraTech.Entities.API.Search.Fare adultfareReturn = new XTraTech.Entities.API.Search.Fare();

                                                if (adults != null)
                                                {
                                                    adultfareReturn.PassengerType = "ADT";
                                                    adultfareReturn.PassengerQuantity = rq.FlightSearchDetails.AirTravelers[0].Quantity;
                                                    adultfareReturn.SinglePassangerBaseFare = new Money
                                                    {
                                                        Amount = (Convert.ToDouble(DepartureResults[i].SelectSingleNode("price_adult").InnerText) + Convert.ToDouble(ReturnResults[j].SelectSingleNode("price_adult").InnerText)).ToString(),
                                                        Currency = "IDR"
                                                    };
                                                    adultfareReturn.SinglePassangerTax = new Money
                                                    {
                                                        Amount = "0.0",
                                                        Currency = "IDR"
                                                    };

                                                    faresReturn.Add(adultfareReturn);
                                                }

                                                if (child != null)
                                                {
                                                    //child here
                                                    childfareReturn.PassengerType = "CHD";
                                                    childfareReturn.PassengerQuantity = rq.FlightSearchDetails.AirTravelers[1].Quantity;
                                                    childfareReturn.SinglePassangerBaseFare = new Money
                                                    {
                                                        Amount = (Convert.ToDouble(DepartureResults[i].SelectSingleNode("price_child").InnerText) + Convert.ToDouble(ReturnResults[j].SelectSingleNode("price_child").InnerText)).ToString(),
                                                        Currency = "IDR"
                                                    };
                                                    childfareReturn.SinglePassangerTax = new Money
                                                    {
                                                        Amount = "0.0",
                                                        Currency = "IDR"
                                                    };

                                                    faresReturn.Add(childfareReturn);
                                                }
                                                if (infant != null)
                                                {
                                                    //infant here
                                                    infantfareReturn.PassengerType = "CHD";
                                                    infantfareReturn.PassengerQuantity = rq.FlightSearchDetails.AirTravelers[0].Quantity;
                                                    infantfareReturn.SinglePassangerBaseFare = new Money
                                                    {
                                                        Amount = (Convert.ToDouble(DepartureResults[i].SelectSingleNode("price_infant").InnerText) + Convert.ToDouble(ReturnResults[j].SelectSingleNode("price_infant").InnerText)).ToString(),
                                                        Currency = "IDR"
                                                    };
                                                    infantfareReturn.SinglePassangerTax = new Money
                                                    {
                                                        Amount = "0.0",
                                                        Currency = "IDR"
                                                    };

                                                    faresReturn.Add(infantfareReturn);
                                                }

                                                fareDetailsReturn.Fares = faresReturn;
                                                airItineraryReturn.FareDetails = fareDetailsReturn;

                                                List<FlightDetail> flightDetailsReturn = new List<FlightDetail>();
                                                FlightDetail flightDetailReturn = new FlightDetail();
                                                flightDetailReturn.JourneyType = "Return";
                                                List<XTraTech.Entities.API.Search.FlightSegment> FlightSementsReturn = new List<XTraTech.Entities.API.Search.FlightSegment>();

                                                XmlNode flightInfosReturn = ReturnResults[j].SelectSingleNode("flight_infos");
                                                XmlNodeList flightInfosListReturn = flightInfosReturn.SelectNodes("flight_info");

                                                for (int onwardLegId = 0; onwardLegId < flightInfosListReturn.Count; onwardLegId++)
                                                {
                                                    XTraTech.Entities.API.Search.FlightSegment ileg = new XTraTech.Entities.API.Search.FlightSegment();
                                                    ileg.JourneyDuration = "400";//Info not exist
                                                    ileg.FlightCode = flightInfosListReturn[onwardLegId].SelectSingleNode("flight_number").InnerText.Split('-')[0];
                                                    ileg.FlightNumber = flightInfosListReturn[onwardLegId].SelectSingleNode("flight_number").InnerText.Split('-')[1];
                                                    ileg.Origin = flightInfosListReturn[onwardLegId].SelectSingleNode("departure_city").InnerText;
                                                    ileg.DepartureDateTime = rq.FlightSearchDetails.AirTravelDetails[1].DepartureDateTime;
                                                    ileg.MarketingAirline = ileg.FlightCode;
                                                    ileg.OperatingAirline = ileg.FlightCode;
                                                    ileg.Destination = flightInfosListReturn[onwardLegId].SelectSingleNode("arrival_city").InnerText;
                                                    ileg.ArrivalDateime = rq.FlightSearchDetails.AirTravelDetails[1].DepartureDateTime;
                                                    ileg.CabinClass = "Y";//info not present
                                                    ileg.BookingClass = "R";//info not present
                                                    ileg.EquipmentType = "747-8";//info not present

                                                    FlightSementsReturn.Add(ileg);
                                                }

                                                flightDetailReturn.FlightSements = FlightSementsReturn;

                                                flightDetailsReturn.Add(flightDetailOnward);
                                                flightDetailsReturn.Add(flightDetailReturn);
                                                airItineraryReturn.FlightDetails = flightDetailsReturn;
                                                itineraries.Add(airItineraryReturn);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
