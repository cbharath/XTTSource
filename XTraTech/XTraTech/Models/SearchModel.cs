using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace XTraTech.Models
{
    public class SearchModel
    {
        public List<CityPair> CityPares
        {
            get;
            set;
        }
        [Display(Name = "Journey Type")]
        public string JourneyType
        {
            get;
            set;
        }
        [Display(Name = "Adult Count")]
        public int AdultCount
        {
            get;
            set;
        }
        [Display(Name = "Child Count")]
        public int ChildCount
        {
            get;
            set;
        }
        [Display(Name = "Infant Count")]
        public int InfantCount
        {
            get;
            set;
        }
        public string CabinClass
        {
            get;
            set;
        }
        public IEnumerable<SelectListItem> AdultList
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "1",
						Text = "1"
					},
					new SelectListItem
					{
						Value = "2",
						Text = "2"
					},
					new SelectListItem
					{
						Value = "3",
						Text = "3"
					},
					new SelectListItem
					{
						Value = "4",
						Text = "4"
					},
					new SelectListItem
					{
						Value = "5",
						Text = "5"
					},
					new SelectListItem
					{
						Value = "6",
						Text = "6"
					},
					new SelectListItem
					{
						Value = "7",
						Text = "7"
					},
					new SelectListItem
					{
						Value = "8",
						Text = "8"
					},
					new SelectListItem
					{
						Value = "9",
						Text = "9"
					}
				};
            }
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
						Value = "VJML",
						Text = "VJML"
					},
					new SelectListItem
					{
						Value = "VOML",
						Text = "VOML"
					},
					new SelectListItem
					{
						Value = "AVML",
						Text = "AVML"
					},
					new SelectListItem
					{
						Value = "BBML",
						Text = "BBML"
					},
					new SelectListItem
					{
						Value = "BLML",
						Text = "BLML"
					},
					new SelectListItem
					{
						Value = "FPML",
						Text = "FPML"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> ChildList
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "0",
						Text = "0"
					},
					new SelectListItem
					{
						Value = "1",
						Text = "1"
					},
					new SelectListItem
					{
						Value = "2",
						Text = "2"
					},
					new SelectListItem
					{
						Value = "3",
						Text = "3"
					},
					new SelectListItem
					{
						Value = "4",
						Text = "4"
					},
					new SelectListItem
					{
						Value = "5",
						Text = "5"
					},
					new SelectListItem
					{
						Value = "6",
						Text = "6"
					},
					new SelectListItem
					{
						Value = "7",
						Text = "7"
					},
					new SelectListItem
					{
						Value = "8",
						Text = "8"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> InfantList
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "0",
						Text = "0"
					},
					new SelectListItem
					{
						Value = "1",
						Text = "1"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> SourceTypeList
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "All",
						Text = "All"
					},
					new SelectListItem
					{
						Value = "Public",
						Text = "Public"
					},
					new SelectListItem
					{
						Value = "Private",
						Text = "Private"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> CabinClassList
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "Economy",
						Text = "Economy"
					},
					new SelectListItem
					{
						Value = "Premium Economy",
						Text = "Premium Economy"
					},
					new SelectListItem
					{
						Value = "Business",
						Text = "Business"
					},
					new SelectListItem
					{
						Value = "First",
						Text = "First"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> RequestOptionList
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "50",
						Text = "50"
					},
					new SelectListItem
					{
						Value = "100",
						Text = "100"
					},
					new SelectListItem
					{
						Value = "200",
						Text = "200"
					}
				};
            }
        }
        public IEnumerable<SelectListItem> StopsList
        {
            get
            {
                return new SelectListItem[]
				{
					new SelectListItem
					{
						Value = "All Flights",
						Text = "All Flights"
					},
					new SelectListItem
					{
						Value = "Direct Flights",
						Text = "Direct Flights"
					},
					new SelectListItem
					{
						Value = "One Stop",
						Text = "One Stop"
					}
				};
            }
        }
    }
    public class CityPair
    {
        [Display(Name = "Origin City")]
        public string Origin
        {
            get;
            set;
        }
        [Display(Name = "Destination City")]
        public string Destination
        {
            get;
            set;
        }
        public DateTime DepartureDateTime
        {
            get;
            set;
        }
        public DateTime ReturnDateTime
        {
            get;
            set;
        }
    }
}