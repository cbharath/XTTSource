using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XTraTech.DAL;
namespace XTraTech.Entities.COM
{
    public class FlightSegment
    {
        public int FlightSegmentID
        {
            get;
            set;
        }
        public int SegmentGroupID
        {
            get;
            set;
        }
        public bool IsReturn
        {
            get;
            set;
        }
        public string Origin
        {
            get;
            set;
        }
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
        public DateTime ArrivalDateime
        {
            get;
            set;
        }
        public string JourneyDuration
        {
            get;
            set;
        }
        public string StopQuantity
        {
            get;
            set;
        }
        public string FlightNumber
        {
            get;
            set;
        }
        public string FlightCode
        {
            get;
            set;
        }
        public string OperatingAirline
        {
            get;
            set;
        }
        public string EquipmentType
        {
            get;
            set;
        }
        public string BookingClass
        {
            get;
            set;
        }
        public string CabinClass
        {
            get;
            set;
        }
        public string MarketingAirline
        {
            get;
            set;
        }
        public string AirlinePNR
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
        public void Save(int FlightDetailID)
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@SegmentGroupID", this.SegmentGroupID));
                sqlParameterList.Add(new SqlParameter("@IsReturn", this.IsReturn));
                sqlParameterList.Add(new SqlParameter("@Origin", this.Origin));
                sqlParameterList.Add(new SqlParameter("@Destination", this.Destination));
                sqlParameterList.Add(new SqlParameter("@DepartureDateTime", this.DepartureDateTime));
                sqlParameterList.Add(new SqlParameter("@ArrivalDateime", this.ArrivalDateime));
                sqlParameterList.Add(new SqlParameter("@JourneyDuration", this.JourneyDuration));
                sqlParameterList.Add(new SqlParameter("@StopQuantity", this.StopQuantity));
                sqlParameterList.Add(new SqlParameter("@FlightNumber", this.FlightNumber));
                sqlParameterList.Add(new SqlParameter("@FlightCode", this.FlightCode));
                sqlParameterList.Add(new SqlParameter("@OperatingAirline", this.OperatingAirline));
                sqlParameterList.Add(new SqlParameter("@EquipmentType", this.EquipmentType));
                sqlParameterList.Add(new SqlParameter("@BookingClass", this.BookingClass));
                sqlParameterList.Add(new SqlParameter("@CabinClass", this.CabinClass));
                sqlParameterList.Add(new SqlParameter("@MarketingAirline", this.MarketingAirline));
                sqlParameterList.Add(new SqlParameter("@AirlinePNR", this.AirlinePNR));
                sqlParameterList.Add(new SqlParameter("@CreatedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@ModefiedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@FlightDetailsID", FlightDetailID));
                SqlParameter paramBookingRef = new SqlParameter("@FlightSegmentID", SqlDbType.Int);
                paramBookingRef.Direction = ParameterDirection.Output;
                sqlParameterList.Add(paramBookingRef);
                SqlHelper.ExecuteNonQuery("InsertFlightSegment", sqlParameterList.ToArray());
                int flightFareID = (int)paramBookingRef.Value;
            }
            catch (SqlException ex_215)
            {
            }
        }
        public List<FlightSegment> Load(int FlightDetailsID)
        {
            DataTable dataTable = new DataTable();
            List<FlightSegment> FlightSegments = new List<FlightSegment>();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetFlightSegments", new List<SqlParameter>
				{
					new SqlParameter("@FlightDetailsID", FlightDetailsID)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    FlightSegments.Add(new FlightSegment
                    {
                        FlightSegmentID = Convert.ToInt32(dataTable.Rows[index]["FlightSegmentID"].ToString()),
                        SegmentGroupID = Convert.ToInt32(dataTable.Rows[index]["SegmentGroupID"].ToString()),
                        IsReturn = Convert.ToBoolean(dataTable.Rows[index]["IsReturn"]),
                        Origin = dataTable.Rows[index]["Origin"].ToString(),
                        Destination = dataTable.Rows[index]["Destination"].ToString(),
                        DepartureDateTime = Convert.ToDateTime(dataTable.Rows[index]["DepartureDateTime"]),
                        ArrivalDateime = Convert.ToDateTime(dataTable.Rows[index]["ArrivalDateime"]),
                        JourneyDuration = dataTable.Rows[index]["JourneyDuration"].ToString(),
                        StopQuantity = dataTable.Rows[index]["StopQuantity"].ToString(),
                        FlightNumber = dataTable.Rows[index]["FlightNumber"].ToString(),
                        FlightCode = dataTable.Rows[index]["FlightCode"].ToString(),
                        OperatingAirline = dataTable.Rows[index]["OperatingAirline"].ToString(),
                        EquipmentType = dataTable.Rows[index]["EquipmentType"].ToString(),
                        BookingClass = dataTable.Rows[index]["BookingClass"].ToString(),
                        CabinClass = dataTable.Rows[index]["CabinClass"].ToString(),
                        MarketingAirline = dataTable.Rows[index]["MarketingAirline"].ToString(),
                        AirlinePNR = dataTable.Rows[index]["AirlinePNR"].ToString(),
                        CreatedOn = Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]),
                        ModefiedOn = Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"])
                    });
                }
            }
            catch (Exception)
            {
            }
            return FlightSegments;
        }
    }
}
