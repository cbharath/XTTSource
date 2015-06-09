using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XTraTech.Entities.API.Search;
using XTraTech.Entities.COM;

namespace XTraTechWeb.Models
{
    public class PassengerModel
    {
        public List<XTraTech.Entities.COM.FlightDetail> SelectedOptions = new List<XTraTech.Entities.COM.FlightDetail>();
        public FareXtractorRq SearchRequest = new FareXtractorRq();
        public List<Passenger> Passengers
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string MobileNum
        {
            get;
            set;
        }
        public string PostalCode
        {
            get;
            set;
        }
        public string Error
        {
            get;
            set;
        }
        public IEnumerable<SelectListItem> SeatPrefList
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "Any",
						Text = "Any"
					},
					new SelectListItem
					{
						Value = "A",
						Text = "A"
					},
					new SelectListItem
					{
						Value = "W",
						Text = "W"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> MealPrefList
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "Any",
						Text = "Any"
					},
					new SelectListItem
					{
						Value = "HFML",
						Text = "HFML"
					},
					new SelectListItem
					{
						Value = "LPML",
						Text = "LPML"
					},
					new SelectListItem
					{
						Value = "ORML",
						Text = "ORML"
					},
					new SelectListItem
					{
						Value = "PRML",
						Text = "PRML"
					},
					new SelectListItem
					{
						Value = "FPML",
						Text = "FPML"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> Titles
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "Mr",
						Text = "Mr"
					},
					new SelectListItem
					{
						Value = "Ms",
						Text = "Ms"
					},
					new SelectListItem
					{
						Value = "Mrs",
						Text = "Mrs"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> Genders
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "Male",
						Text = "Male"
					},
					new SelectListItem
					{
						Value = "Female",
						Text = "Female"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> Days
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "00",
						Text = "DD"
					},
					new SelectListItem
					{
						Value = "01",
						Text = "01"
					},
					new SelectListItem
					{
						Value = "02",
						Text = "02"
					},
					new SelectListItem
					{
						Value = "03",
						Text = "03"
					},
					new SelectListItem
					{
						Value = "04",
						Text = "04"
					},
					new SelectListItem
					{
						Value = "05",
						Text = "05"
					},
					new SelectListItem
					{
						Value = "06",
						Text = "06"
					},
					new SelectListItem
					{
						Value = "07",
						Text = "07"
					},
					new SelectListItem
					{
						Value = "08",
						Text = "08"
					},
					new SelectListItem
					{
						Value = "09",
						Text = "09"
					},
					new SelectListItem
					{
						Value = "10",
						Text = "10"
					},
					new SelectListItem
					{
						Value = "11",
						Text = "11"
					},
					new SelectListItem
					{
						Value = "12",
						Text = "12"
					},
					new SelectListItem
					{
						Value = "13",
						Text = "13"
					},
					new SelectListItem
					{
						Value = "14",
						Text = "14"
					},
					new SelectListItem
					{
						Value = "15",
						Text = "15"
					},
					new SelectListItem
					{
						Value = "16",
						Text = "16"
					},
					new SelectListItem
					{
						Value = "17",
						Text = "17"
					},
					new SelectListItem
					{
						Value = "18",
						Text = "18"
					},
					new SelectListItem
					{
						Value = "19",
						Text = "19"
					},
					new SelectListItem
					{
						Value = "20",
						Text = "20"
					},
					new SelectListItem
					{
						Value = "21",
						Text = "21"
					},
					new SelectListItem
					{
						Value = "22",
						Text = "22"
					},
					new SelectListItem
					{
						Value = "23",
						Text = "23"
					},
					new SelectListItem
					{
						Value = "24",
						Text = "24"
					},
					new SelectListItem
					{
						Value = "25",
						Text = "25"
					},
					new SelectListItem
					{
						Value = "20",
						Text = "20"
					},
					new SelectListItem
					{
						Value = "21",
						Text = "21"
					},
					new SelectListItem
					{
						Value = "22",
						Text = "22"
					},
					new SelectListItem
					{
						Value = "23",
						Text = "23"
					},
					new SelectListItem
					{
						Value = "24",
						Text = "24"
					},
					new SelectListItem
					{
						Value = "25",
						Text = "25"
					},
					new SelectListItem
					{
						Value = "26",
						Text = "26"
					},
					new SelectListItem
					{
						Value = "27",
						Text = "27"
					},
					new SelectListItem
					{
						Value = "28",
						Text = "28"
					},
					new SelectListItem
					{
						Value = "29",
						Text = "29"
					},
					new SelectListItem
					{
						Value = "30",
						Text = "30"
					},
					new SelectListItem
					{
						Value = "31",
						Text = "31"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> Months
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "00",
						Text = "MMM"
					},
					new SelectListItem
					{
						Value = "01",
						Text = "JAN"
					},
					new SelectListItem
					{
						Value = "02",
						Text = "FEB"
					},
					new SelectListItem
					{
						Value = "03",
						Text = "MAR"
					},
					new SelectListItem
					{
						Value = "04",
						Text = "APR"
					},
					new SelectListItem
					{
						Value = "05",
						Text = "MAY"
					},
					new SelectListItem
					{
						Value = "06",
						Text = "JUN"
					},
					new SelectListItem
					{
						Value = "07",
						Text = "JUL"
					},
					new SelectListItem
					{
						Value = "08",
						Text = "AUG"
					},
					new SelectListItem
					{
						Value = "09",
						Text = "SEP"
					},
					new SelectListItem
					{
						Value = "10",
						Text = "OCT"
					},
					new SelectListItem
					{
						Value = "11",
						Text = "NOV"
					},
					new SelectListItem
					{
						Value = "12",
						Text = "DEC"
					}
				};
            }
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

    public class Passenger
    {
        public string Title
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string Day
        {
            get;
            set;
        }
        public string Month
        {
            get;
            set;
        }
        public string Year
        {
            get;
            set;
        }
        public string PassportNum
        {
            get;
            set;
        }
        public string Country
        {
            get;
            set;
        }
        public string Nationality
        {
            get;
            set;
        }
        public string PDay
        {
            get;
            set;
        }
        public string PMonth
        {
            get;
            set;
        }
        public string PYear
        {
            get;
            set;
        }
        public string MealReq
        {
            get;
            set;
        }
        public string SeatPref
        {
            get;
            set;
        }
        public string FFNum
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }
    }
}