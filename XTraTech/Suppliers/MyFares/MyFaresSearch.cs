using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using XTraTech.Entities.API.Search;
namespace MyFares
{
    public class MyFaresSearch
    {
        private OnePoint onePoint = new OnePoint();
        public void Search(FareXtractorRq rq, List<AirItinerary> itineraries)
        {
            //FareXtractorRs fareXtractorRs = new FareXtractorRs();
            AirLowFareSearchRQ searchRq = new AirLowFareSearchRQ();
            AirLowFareSearchRS searchRS = new AirLowFareSearchRS();
            searchRq.IsRefundable = false;
            searchRq.IsRefundableSpecified = true;
            searchRq.NearByAirports = false;
            searchRq.NearByAirportsSpecified = true;
            int passengerTypeCount = 1;
            Passenger adultCount = (
                from p in rq.FlightSearchDetails.AirTravelers
                where p.Code == "ADT"
                select p).FirstOrDefault<Passenger>();
            Passenger childCount = (
                from p in rq.FlightSearchDetails.AirTravelers
                where p.Code == "CHD"
                select p).FirstOrDefault<Passenger>();
            if (childCount != null)
            {
                passengerTypeCount++;
            }
            Passenger infantCount = (
                from p in rq.FlightSearchDetails.AirTravelers
                where p.Code == "INF"
                select p).FirstOrDefault<Passenger>();
            if (infantCount != null)
            {
                passengerTypeCount++;
            }
            PassengerTypeQuantity[] passengerTypeQuantityList = new PassengerTypeQuantity[passengerTypeCount];
            passengerTypeQuantityList[0] = new PassengerTypeQuantity
            {
                Code = PassengerType.ADT,
                CodeSpecified = true,
                Quantity = Convert.ToInt32(adultCount.Quantity),
                QuantitySpecified = true
            };
            if (childCount != null)
            {
                passengerTypeQuantityList[1] = new PassengerTypeQuantity
                {
                    Code = PassengerType.CHD,
                    CodeSpecified = true,
                    Quantity = Convert.ToInt32(childCount.Quantity),
                    QuantitySpecified = true
                };
            }
            if (infantCount != null)
            {
                passengerTypeQuantityList[2] = new PassengerTypeQuantity
                {
                    Code = PassengerType.INF,
                    CodeSpecified = true,
                    Quantity = Convert.ToInt32(infantCount.Quantity),
                    QuantitySpecified = true
                };
            }
            searchRq.PassengerTypeQuantities = passengerTypeQuantityList;
            int originDestinationCount = 1;
            if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "RETURN")
            {
                originDestinationCount = 2;
            }
            if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "CIRCLE" || rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "OPENJAW")
            {
                originDestinationCount = rq.FlightSearchDetails.AirTravelDetails.Count;
            }
            OriginDestinationInformation[] originDestinationInformationList = new OriginDestinationInformation[originDestinationCount];
            TravelPreferences travelPreference = new TravelPreferences();
            if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "ONEWAY")
            {
                travelPreference.AirTripType = AirTripType.OneWay;
                travelPreference.AirTripTypeSpecified = true;
                travelPreference.CabinPreference = CabinType.Y;
                travelPreference.CabinPreferenceSpecified = true;
                travelPreference.MaxStopsQuantity = MaxStopsQuantity.All;
                travelPreference.MaxStopsQuantitySpecified = true;
                originDestinationInformationList[0] = new OriginDestinationInformation
                {
                    DepartureDateTime = rq.FlightSearchDetails.AirTravelDetails[0].DepartureDateTime,
                    DepartureDateTimeSpecified = true,
                    OriginLocationCode = rq.FlightSearchDetails.AirTravelDetails[0].Origin,
                    DestinationLocationCode = rq.FlightSearchDetails.AirTravelDetails[0].Destination
                };
            }
            if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "RETURN")
            {
                travelPreference.AirTripType = AirTripType.Return;
                travelPreference.AirTripTypeSpecified = true;
                travelPreference.CabinPreference = CabinType.Y;
                travelPreference.CabinPreferenceSpecified = true;
                travelPreference.MaxStopsQuantity = MaxStopsQuantity.All;
                travelPreference.MaxStopsQuantitySpecified = true;
                originDestinationInformationList[0] = new OriginDestinationInformation
                {
                    DepartureDateTime = rq.FlightSearchDetails.AirTravelDetails[0].DepartureDateTime,
                    DepartureDateTimeSpecified = true,
                    OriginLocationCode = rq.FlightSearchDetails.AirTravelDetails[0].Origin,
                    DestinationLocationCode = rq.FlightSearchDetails.AirTravelDetails[0].Destination
                };
                originDestinationInformationList[1] = new OriginDestinationInformation
                {
                    DepartureDateTime = rq.FlightSearchDetails.AirTravelDetails[1].DepartureDateTime,
                    DepartureDateTimeSpecified = true,
                    OriginLocationCode = rq.FlightSearchDetails.AirTravelDetails[1].Origin,
                    DestinationLocationCode = rq.FlightSearchDetails.AirTravelDetails[1].Destination
                };
            }
            if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "CIRCLE" || rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "OPENJAW")
            {
                travelPreference.AirTripType = AirTripType.OpenJaw;
                travelPreference.AirTripTypeSpecified = true;
                travelPreference.CabinPreference = CabinType.Y;
                travelPreference.CabinPreferenceSpecified = true;
                travelPreference.MaxStopsQuantity = MaxStopsQuantity.All;
                travelPreference.MaxStopsQuantitySpecified = true;
                for (int i = 0; i < rq.FlightSearchDetails.AirTravelDetails.Count; i++)
                {
                    originDestinationInformationList[i] = new OriginDestinationInformation
                    {
                        DepartureDateTime = rq.FlightSearchDetails.AirTravelDetails[i].DepartureDateTime,
                        DepartureDateTimeSpecified = true,
                        OriginLocationCode = rq.FlightSearchDetails.AirTravelDetails[i].Origin,
                        DestinationLocationCode = rq.FlightSearchDetails.AirTravelDetails[i].Destination
                    };
                }
            }
            searchRq.OriginDestinationInformations = originDestinationInformationList;
            searchRq.TravelPreferences = travelPreference;
            searchRq.PricingSourceType = PricingSourceType.All;
            searchRq.PricingSourceTypeSpecified = true;
            searchRq.RequestOptions = RequestOptions.TwoHundred;
            searchRq.RequestOptionsSpecified = true;
            MYAuthentication auth = new MYAuthentication();
            SessionCreateRS session = auth.CreateSession();
            searchRq.SessionId = session.SessionId;
            searchRq.Target = Target.Test;
            searchRq.TargetSpecified = true;
            searchRS = this.onePoint.AirLowFareSearch(searchRq);
            //List<AirItinerary> itineraries = new List<AirItinerary>();
            if (searchRS != null)
            {
                for (int i = 0; i < searchRS.PricedItineraries.Length; i++)
                {
                    AirItinerary airItinerary = new AirItinerary();
                    airItinerary.SecquenceNumber = (i + 1).ToString();
                    airItinerary.ItineraryID = airItinerary.SecquenceNumber + "_" + searchRS.PricedItineraries[i].AirItineraryPricingInfo.FareSourceCode;
                    FareDetails fareDetails = new FareDetails();
                    fareDetails.FareType = "Public";
                    List<XTraTech.Entities.API.Search.Fare> fares = new List<XTraTech.Entities.API.Search.Fare>();
                    for (int fareindex = 0; fareindex < searchRS.PricedItineraries[i].AirItineraryPricingInfo.PTC_FareBreakdowns.Length; fareindex++)
                    {
                        XTraTech.Entities.API.Search.Fare fare = new XTraTech.Entities.API.Search.Fare();
                        if (searchRS.PricedItineraries[i].AirItineraryPricingInfo.PTC_FareBreakdowns[fareindex].PassengerTypeQuantity.Code == PassengerType.ADT)
                        {
                            fare.PassengerType = "ADT";
                        }
                        if (searchRS.PricedItineraries[i].AirItineraryPricingInfo.PTC_FareBreakdowns[fareindex].PassengerTypeQuantity.Code == PassengerType.CHD)
                        {
                            fare.PassengerType = "CHD";
                        }
                        if (searchRS.PricedItineraries[i].AirItineraryPricingInfo.PTC_FareBreakdowns[fareindex].PassengerTypeQuantity.Code == PassengerType.INF)
                        {
                            fare.PassengerType = "INF";
                        }
                        fare.PassengerQuantity = searchRS.PricedItineraries[i].AirItineraryPricingInfo.PTC_FareBreakdowns[fareindex].PassengerTypeQuantity.Quantity.ToString();
                        fare.SinglePassangerBaseFare = new Money
                        {
                            Amount = searchRS.PricedItineraries[i].AirItineraryPricingInfo.PTC_FareBreakdowns[fareindex].PassengerFare.EquivFare.Amount.ToString(),
                            Currency = searchRS.PricedItineraries[i].AirItineraryPricingInfo.PTC_FareBreakdowns[fareindex].PassengerFare.EquivFare.CurrencyCode
                        };
                        Money taxes = new Money();
                        decimal totaltax = 0m;
                        Tax[] taxes2 = searchRS.PricedItineraries[i].AirItineraryPricingInfo.PTC_FareBreakdowns[fareindex].PassengerFare.Taxes;
                        for (int k = 0; k < taxes2.Length; k++)
                        {
                            Tax item = taxes2[k];
                            totaltax += Convert.ToDecimal(item.Amount);
                        }
                        taxes.Amount = totaltax.ToString();
                        taxes.Currency = searchRS.PricedItineraries[i].AirItineraryPricingInfo.PTC_FareBreakdowns[fareindex].PassengerFare.EquivFare.CurrencyCode;
                        fare.SinglePassangerTax = taxes;
                        fares.Add(fare);
                    }
                    fareDetails.Fares = fares;
                    airItinerary.FareDetails = fareDetails;
                    List<FlightDetail> flightDetails = new List<FlightDetail>();
                    if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "ONEWAY" || rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "RETURN")
                    {
                        FlightDetail flightDetailOnward = new FlightDetail();
                        flightDetailOnward.JourneyType = "Onward";
                        List<XTraTech.Entities.API.Search.FlightSegment> FlightSements = new List<XTraTech.Entities.API.Search.FlightSegment>();
                        for (int oriSegments = 0; oriSegments < searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments.Length; oriSegments++)
                        {
                            XTraTech.Entities.API.Search.FlightSegment ileg = new XTraTech.Entities.API.Search.FlightSegment();
                            ileg.JourneyDuration = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].JourneyDuration.ToString();
                            ileg.FlightCode = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].OperatingAirline.Code;
                            ileg.FlightNumber = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].OperatingAirline.FlightNumber;
                            ileg.Origin = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].DepartureAirportLocationCode;
                            ileg.DepartureDateTime = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].DepartureDateTime;
                            ileg.MarketingAirline = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].MarketingAirlineCode;
                            ileg.OperatingAirline = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].OperatingAirline.Code;
                            ileg.Destination = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].ArrivalAirportLocationCode;
                            ileg.ArrivalDateime = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].ArrivalDateTime;
                            ileg.CabinClass = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].CabinClassCode;
                            ileg.BookingClass = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].ResBookDesigCode;
                            if (searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].OperatingAirline != null)
                            {
                                ileg.EquipmentType = searchRS.PricedItineraries[i].OriginDestinationOptions[0].FlightSegments[oriSegments].OperatingAirline.Equipment;
                            }
                            FlightSements.Add(ileg);
                        }
                        flightDetailOnward.FlightSements = FlightSements;
                        flightDetails.Add(flightDetailOnward);
                    }
                    if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "RETURN")
                    {
                        FlightDetail flightDetailRestination = new FlightDetail();
                        flightDetailRestination.JourneyType = "Return";
                        List<XTraTech.Entities.API.Search.FlightSegment> FlightSements = new List<XTraTech.Entities.API.Search.FlightSegment>();
                        for (int oriSegments = 0; oriSegments < searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments.Length; oriSegments++)
                        {
                            XTraTech.Entities.API.Search.FlightSegment ileg = new XTraTech.Entities.API.Search.FlightSegment();
                            ileg.JourneyDuration = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].JourneyDuration.ToString();
                            ileg.FlightCode = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].OperatingAirline.Code;
                            ileg.FlightNumber = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].OperatingAirline.FlightNumber;
                            ileg.Origin = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].DepartureAirportLocationCode;
                            ileg.DepartureDateTime = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].DepartureDateTime;
                            ileg.MarketingAirline = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].MarketingAirlineCode;
                            ileg.OperatingAirline = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].OperatingAirline.Code;
                            ileg.Destination = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].ArrivalAirportLocationCode;
                            ileg.ArrivalDateime = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].ArrivalDateTime;
                            ileg.CabinClass = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].CabinClassCode;
                            ileg.BookingClass = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].ResBookDesigCode;
                            if (searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].OperatingAirline != null)
                            {
                                ileg.EquipmentType = searchRS.PricedItineraries[i].OriginDestinationOptions[1].FlightSegments[oriSegments].OperatingAirline.Equipment;
                            }
                            FlightSements.Add(ileg);
                        }
                        flightDetailRestination.FlightSements = FlightSements;
                        flightDetails.Add(flightDetailRestination);
                    }
                    if (rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "CIRCLE" || rq.FlightSearchDetails.AirTravelType.Trim().ToUpper() == "OPENJAW")
                    {
                        for (int j = 0; j < searchRS.PricedItineraries[i].OriginDestinationOptions.Length; j++)
                        {
                            FlightDetail flightDetailMultidestination = new FlightDetail();
                            flightDetailMultidestination.JourneyType = "Multidestination";
                            List<XTraTech.Entities.API.Search.FlightSegment> FlightSements = new List<XTraTech.Entities.API.Search.FlightSegment>();
                            for (int oriSegments = 0; oriSegments < searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments.Length; oriSegments++)
                            {
                                XTraTech.Entities.API.Search.FlightSegment ileg = new XTraTech.Entities.API.Search.FlightSegment();
                                ileg.JourneyDuration = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].JourneyDuration.ToString();
                                ileg.FlightCode = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].OperatingAirline.Code;
                                ileg.FlightNumber = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].OperatingAirline.FlightNumber;
                                ileg.Origin = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].DepartureAirportLocationCode;
                                ileg.DepartureDateTime = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].DepartureDateTime;
                                ileg.MarketingAirline = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].MarketingAirlineCode;
                                ileg.OperatingAirline = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].OperatingAirline.Code;
                                ileg.Destination = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].ArrivalAirportLocationCode;
                                ileg.ArrivalDateime = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].ArrivalDateTime;
                                ileg.CabinClass = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].CabinClassCode;
                                ileg.BookingClass = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].ResBookDesigCode;
                                if (searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].OperatingAirline != null)
                                {
                                    ileg.EquipmentType = searchRS.PricedItineraries[i].OriginDestinationOptions[j].FlightSegments[oriSegments].OperatingAirline.Equipment;
                                }
                                FlightSements.Add(ileg);
                            }
                            flightDetailMultidestination.FlightSements = FlightSements;
                            flightDetails.Add(flightDetailMultidestination);
                        }
                    }
                    airItinerary.FlightDetails = flightDetails;
                    itineraries.Add(airItinerary);
                }
            }
            //XmlSerializer serializer = new XmlSerializer(typeof(FareXtractorRs));
            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Encoding = new UnicodeEncoding(false, false);
            //settings.Indent = true;
            //settings.OmitXmlDeclaration = true;
            //using (StringWriter textWriter = new StringWriter())
            //{
            //    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
            //    {
            //        serializer.Serialize(xmlWriter, fareXtractorRs);
            //    }
            //    string AnewSearchXML = textWriter.ToString();
            //}
            //return fareXtractorRs;
        }
    }
}
