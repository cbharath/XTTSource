using MyFares;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XTraTech.Entities.API.Search;
using XTraTech.Entities.COM;
using Tickets.ID;
using XTraTech.Helper;

namespace XTraTech.FlightCore
{
    public class FlightSearch
    {
        public FareXtractorRs rs = new FareXtractorRs();
        public List<AirItinerary> itineraries = new List<AirItinerary>();
        public void GetFlightResults(FareXtractorRq rq)
        {
            List<Task> taskList = new List<Task>();
            string strUniqueIdentifier = Guid.NewGuid().ToString();
            //Mystifly Tasks

            //if (Convert.ToBoolean(Configuration.XTraTechConfigurationSettings["EnableMystifly"]))
            {
                Task myTask = Task.Factory.StartNew(delegate
                {
                    this.GetFlightResultsFromMy(rq, itineraries);
                });
                taskList.Add(myTask);
            }

            //Tickets Task
            //if (Convert.ToBoolean(Configuration.XTraTechConfigurationSettings["EnableTickets"]))
            {
                Task ticketsTask = Task.Factory.StartNew(delegate
                {
                    this.GetFlightResultsFromTickets(rq, itineraries);
                });
                taskList.Add(ticketsTask);
            }

            Task.WaitAll(taskList.ToArray());
            this.rs.SearchID = strUniqueIdentifier;
            this.rs.Version = "1.0.0";
            this.rs.Itineraries = itineraries;
            if (this.rs != null)
            {
                if (this.rs.Itineraries != null && this.rs.Itineraries.Count > 0)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<AirItinerary>), new Type[]
					{
						typeof(AirItinerary),
						typeof(AirItinerary)
					});
                    StringWriter writer = new StringWriter();
                    serializer.Serialize(writer, this.rs.Itineraries);
                    string serializedXML = string.Empty;
                    serializedXML = writer.ToString();
                    Itinerary gcmCore = new Itinerary();
                    gcmCore.SearchXML = serializedXML;
                    gcmCore.SearchID = strUniqueIdentifier;

                    Thread gcmThread = new Thread(() => gcmCore.Save());
                    gcmThread.IsBackground = true;
                    gcmThread.Start();
                }
            }
        }
        //private void GetFlightResultsFromContentFarmer(FareXtractorRq rq)
        //{
        //    FareNetSearch search = new FareNetSearch();
        //    this.rs = search.Search(rq);
        //}
        private void GetFlightResultsFromMy(FareXtractorRq rq, List<AirItinerary> itineraries)
        {
            MyFaresSearch search = new MyFaresSearch();
            search.Search(rq, itineraries);
        }

        private void GetFlightResultsFromTickets(FareXtractorRq rq, List<AirItinerary> itineraries)
        {
            TicketsSearch search = new TicketsSearch();
            search.Search(rq, itineraries);
        }
    }
}
