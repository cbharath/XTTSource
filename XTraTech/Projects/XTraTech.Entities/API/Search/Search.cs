using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace XTraTech.Entities.API.Search
{
    [DataContract]
    public class AirItinerary
    {
        [DataMember]
        public List<FlightDetail> FlightDetails = new List<FlightDetail>();
        [DataMember]
        public List<string> RequiredFieds = new List<string>();
        [DataMember]
        public FareDetails FareDetails = new FareDetails();
        [DataMember, XmlAttribute("SecquenceNumber")]
        public string SecquenceNumber
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("ItineraryID")]
        public string ItineraryID
        {
            get;
            set;
        }
    }

    [DataContract]
    public class AirTravelPreference
    {
        [DataMember]
        public string CabinClass
        {
            get;
            set;
        }
        [DataMember]
        public string AirlinePreference
        {
            get;
            set;
        }
    }

    [DataContract]
    public class Fare
    {
        [DataMember, XmlAttribute("PassengerType")]
        public string PassengerType
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("PassengerQuantity")]
        public string PassengerQuantity
        {
            get;
            set;
        }
        [DataMember]
        public Money SinglePassangerBaseFare
        {
            get;
            set;
        }
        [DataMember]
        public Money SinglePassangerTax
        {
            get;
            set;
        }
    }
    [DataContract]
    public class FareDetails
    {
        [DataMember]
        public List<Fare> Fares = new List<Fare>();
        [DataMember, XmlAttribute("FareType")]
        public string FareType
        {
            get;
            set;
        }
    }
    [DataContract, XmlRoot("FareXtractorRq")]
    [Serializable]
    public class FareXtractorRq
    {
        [DataMember]
        public AuthenticationDetails AuthenticationDetails = new AuthenticationDetails();
        [DataMember]
        public FlightSearchDetails FlightSearchDetails = new FlightSearchDetails();
        [DataMember, XmlAttribute("Version")]
        public string Version
        {
            get;
            set;
        }
    }
    [DataContract]
    public class FareXtractorRs
    {
        [DataMember]
        public List<Message> Messages = new List<Message>();
        [DataMember]
        public List<AirItinerary> Itineraries = new List<AirItinerary>();
        [DataMember, XmlAttribute("Version")]
        public string Version
        {
            get;
            set;
        }
        [DataMember]
        public string SearchID
        {
            get;
            set;
        }
    }
    [DataContract]
    public class FlightDetail
    {
        [DataMember]
        public List<FlightSegment> FlightSements = new List<FlightSegment>();
        [DataMember, XmlAttribute("JourneyType")]
        public string JourneyType
        {
            get;
            set;
        }
    }
    [DataContract]
    public class FlightSearchDetails
    {
        [DataMember]
        public List<OriginDestinationInformation> AirTravelDetails = new List<OriginDestinationInformation>();
        [DataMember]
        public List<Passenger> AirTravelers = new List<Passenger>();
        [DataMember]
        public AirTravelPreference AirTravelPreference = new AirTravelPreference();
        [DataMember]
        public string AirTravelType
        {
            get;
            set;
        }
    }
    [DataContract]
    public class FlightSegment
    {
        [DataMember, XmlAttribute("Origin")]
        public string Origin
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("Destination")]
        public string Destination
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("DepartureDateTime")]
        public DateTime DepartureDateTime
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("ArrivalDateime")]
        public DateTime ArrivalDateime
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("JourneyDuration")]
        public string JourneyDuration
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("StopQuantity")]
        public string StopQuantity
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("FlightNumber")]
        public string FlightNumber
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("FlightCode")]
        public string FlightCode
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("OperatingAirline")]
        public string OperatingAirline
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("EquipmentType")]
        public string EquipmentType
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("BookingClass")]
        public string BookingClass
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("CabinClass")]
        public string CabinClass
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("MarketingAirline")]
        public string MarketingAirline
        {
            get;
            set;
        }
    }
    [DataContract]
    public class Money
    {
        [DataMember, XmlAttribute("Amount")]
        public string Amount
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("Currency")]
        public string Currency
        {
            get;
            set;
        }
    }
    [DataContract]
    public class OriginDestinationInformation
    {
        [DataMember]
        public DateTime DepartureDateTime
        {
            get;
            set;
        }
        [DataMember]
        public string Origin
        {
            get;
            set;
        }
        [DataMember]
        public string Destination
        {
            get;
            set;
        }
    }
    [DataContract]
    public class Passenger
    {
        [DataMember, XmlAttribute("Code")]
        public string Code
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("Quantity")]
        public string Quantity
        {
            get;
            set;
        }
    }
}
