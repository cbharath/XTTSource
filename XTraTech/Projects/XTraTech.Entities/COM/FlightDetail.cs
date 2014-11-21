using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XTraTech.DAL;
namespace XTraTech.Entities.COM
{
    public class FlightDetail
    {
        public int FlightDetailsID
        {
            get;
            set;
        }
        public DateTime TktTimeLimit
        {
            get;
            set;
        }
        public string PNR
        {
            get;
            set;
        }
        public XTraTech.Entities.COM.Enumaration.BookingStatus Status
        {
            get;
            set;
        }
        public DateTime CreatedOn
        {
            get;
            set;
        }
        public DateTime ModefiedOn
        {
            get;
            set;
        }
        public string ItineraryID
        {
            get;
            set;
        }
        public List<FlightSegment> FlightSegments
        {
            get;
            set;
        }
        public List<FlightFare> FlightFares
        {
            get;
            set;
        }
        public void Save(int PurchaseOrderID)
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@TktTimeLimit", this.TktTimeLimit));
                sqlParameterList.Add(new SqlParameter("@PNR", this.PNR));
                sqlParameterList.Add(new SqlParameter("@Status", this.Status));
                sqlParameterList.Add(new SqlParameter("@CreatedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@ModefiedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@ItineraryID", this.ItineraryID));
                sqlParameterList.Add(new SqlParameter("@PurchaseOrderID", PurchaseOrderID));
                SqlParameter paramBookingRef = new SqlParameter("@FlightDetailsID", SqlDbType.Int);
                paramBookingRef.Direction = ParameterDirection.Output;
                sqlParameterList.Add(paramBookingRef);
                SqlHelper.ExecuteNonQuery("InsertFlightDetails", sqlParameterList.ToArray());
                int flightID = (int)paramBookingRef.Value;
                foreach (FlightSegment item in this.FlightSegments)
                {
                    FlightSegment flightSegment = new FlightSegment();
                    flightSegment = item;
                    flightSegment.Save(flightID);
                }
                foreach (FlightFare item2 in this.FlightFares)
                {
                    FlightFare flightFare = new FlightFare();
                    flightFare = item2;
                    flightFare.Save(flightID);
                }
            }
            catch (SqlException ex_191)
            {
            }
        }
        public List<FlightDetail> Load(int purchaseOrderID, bool DoLoadFlightSegment, bool DoLoadFlightFare)
        {
            DataTable dataTable = new DataTable();
            List<FlightDetail> FlightDetails = new List<FlightDetail>();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetFlightDetails", new List<SqlParameter>
				{
					new SqlParameter("@PurchaseOrderID", purchaseOrderID)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    FlightDetail flightDetail = new FlightDetail();
                    flightDetail.FlightDetailsID = Convert.ToInt32(dataTable.Rows[index]["FlightDetailsID"].ToString());
                    flightDetail.TktTimeLimit = Convert.ToDateTime(dataTable.Rows[index]["TktTimeLimit"].ToString());
                    flightDetail.PNR = dataTable.Rows[index]["PNR"].ToString();
                    flightDetail.Status = (XTraTech.Entities.COM.Enumaration.BookingStatus)Convert.ToInt32(dataTable.Rows[index]["Status"]);
                    flightDetail.ItineraryID = dataTable.Rows[index]["ItineraryID"].ToString();
                    flightDetail.CreatedOn = Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]);
                    flightDetail.ModefiedOn = Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"]);
                    if (DoLoadFlightSegment)
                    {
                        FlightSegment flightSegment = new FlightSegment();
                        this.FlightSegments = flightSegment.Load(flightDetail.FlightDetailsID);
                        flightDetail.FlightSegments = this.FlightSegments;
                    }
                    if (DoLoadFlightFare)
                    {
                        FlightFare flightFare = new FlightFare();
                        this.FlightFares = flightFare.Load(flightDetail.FlightDetailsID);
                        flightDetail.FlightFares = this.FlightFares;
                    }
                    FlightDetails.Add(flightDetail);
                }
            }
            catch (Exception)
            {
            }
            return FlightDetails;
        }
        public string GetFare(List<FlightFare> flightFare, string content)
        {
            return Helper.GetFare(flightFare, content);
        }
    }
}
