using System;
using System.Collections.Generic;
using System.Linq;
using XTraTech.Entities.API.Search;
using XTraTech.Entities.COM;

namespace XTraTech.Models
{
    public class SearchResponse
    {
        public List<XTraTech.Entities.COM.FlightDetail> SearchResults = new List<XTraTech.Entities.COM.FlightDetail>();
        public FareXtractorRq SearchRequest = new FareXtractorRq();
        public string PageName
        {
            get;
            set;
        }
        public XTraTech.Entities.COM.FlightDetail ConvertFromAirItinerarytoFlightDetails(AirItinerary flightSearchResult)
        {
            XTraTech.Entities.COM.FlightDetail flightDetail = new XTraTech.Entities.COM.FlightDetail();
            flightDetail.ItineraryID = flightSearchResult.ItineraryID;
            List<XTraTech.Entities.COM.FlightSegment> list = new List<XTraTech.Entities.COM.FlightSegment>();
            for (int i = 0; i < flightSearchResult.FlightDetails.Count; i++)
            {
                if (flightSearchResult.FlightDetails[i].JourneyType.Trim().ToUpper() == "ONWARD")
                {
                    for (int j = 0; j < flightSearchResult.FlightDetails[i].FlightSements.Count; j++)
                    {
                        XTraTech.Entities.COM.FlightSegment item = new XTraTech.Entities.COM.FlightSegment();
                        item = this.GetFlightSegment(flightSearchResult, i, j, false, 1);
                        list.Add(item);
                    }
                }
                else
                {
                    for (int k = 0; k < flightSearchResult.FlightDetails[i].FlightSements.Count; k++)
                    {
                        XTraTech.Entities.COM.FlightSegment item2 = new XTraTech.Entities.COM.FlightSegment();
                        item2 = this.GetFlightSegment(flightSearchResult, i, k, true, 2);
                        list.Add(item2);
                    }
                }
            }
            flightDetail.FlightSegments = list;
            List<FlightFare> list2 = new List<FlightFare>();
            for (int l = 0; l < flightSearchResult.FareDetails.Fares.Count<Fare>(); l++)
            {
                FlightFare flightFare = new FlightFare();
                flightFare.BaseFare = Convert.ToDecimal(flightSearchResult.FareDetails.Fares[l].SinglePassangerBaseFare.Amount);
                flightFare.Tax = Convert.ToDecimal(flightSearchResult.FareDetails.Fares[l].SinglePassangerTax.Amount);
                flightFare.PaxCount = Convert.ToInt32(flightSearchResult.FareDetails.Fares[l].PassengerQuantity);
                if (flightSearchResult.FareDetails.Fares[l].PassengerType.Trim().ToUpper() == "ADT")
                {
                    flightFare.PaxType = XTraTech.Entities.COM.Enumaration.PaxType.ADT;
                }
                if (flightSearchResult.FareDetails.Fares[l].PassengerType.Trim().ToUpper() == "CHD")
                {
                    flightFare.PaxType = XTraTech.Entities.COM.Enumaration.PaxType.CHD;
                }
                if (flightSearchResult.FareDetails.Fares[l].PassengerType.Trim().ToUpper() == "INF")
                {
                    flightFare.PaxType = XTraTech.Entities.COM.Enumaration.PaxType.INF;
                }
                flightFare.FareType = flightSearchResult.FareDetails.FareType;
                flightFare.Currency = flightSearchResult.FareDetails.Fares[l].SinglePassangerBaseFare.Currency;
                list2.Add(flightFare);
            }
            flightDetail.FlightFares = list2;
            return flightDetail;
        }
        private XTraTech.Entities.COM.FlightSegment GetFlightSegment(AirItinerary flightSearchResult, int flightdetailindex, int FlightSegmentIndex, bool IsReturn, int SegmentGroupID)
        {
            return new XTraTech.Entities.COM.FlightSegment
            {
                ArrivalDateime = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].ArrivalDateime,
                BookingClass = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].BookingClass,
                CabinClass = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].CabinClass,
                DepartureDateTime = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].DepartureDateTime,
                Destination = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].Destination,
                EquipmentType = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].EquipmentType,
                FlightCode = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].FlightCode,
                FlightNumber = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].FlightNumber,
                IsReturn = IsReturn,
                JourneyDuration = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].JourneyDuration,
                MarketingAirline = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].MarketingAirline,
                OperatingAirline = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].OperatingAirline,
                Origin = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].Origin,
                SegmentGroupID = SegmentGroupID,
                StopQuantity = flightSearchResult.FlightDetails[flightdetailindex].FlightSements[FlightSegmentIndex].StopQuantity
            };
        }
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