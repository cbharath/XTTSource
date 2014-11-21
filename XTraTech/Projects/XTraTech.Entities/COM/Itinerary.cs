using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XTraTech.DAL;
namespace XTraTech.Entities.COM
{
    public class Itinerary
    {
        public string ItineraryID
        {
            get;
            set;
        }
        public string SearchID
        {
            get;
            set;
        }
        public string SearchXML
        {
            get;
            set;
        }
        public bool IsBooked
        {
            get;
            set;
        }
        public void Save()
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@ItineraryID", "1"));
                sqlParameterList.Add(new SqlParameter("@SerializedXML", XTraTech.Helper.Helper.Zip(this.SearchXML)));
                sqlParameterList.Add(new SqlParameter("@SearchID", this.SearchID));
                sqlParameterList.Add(new SqlParameter("@IsBooked", false));
                SqlParameter paramBookingRef = new SqlParameter("@ID", SqlDbType.Int);
                paramBookingRef.Direction = ParameterDirection.Output;
                sqlParameterList.Add(paramBookingRef);
                SqlHelper.ExecuteNonQuery("InsertItinerary", sqlParameterList.ToArray());
                int flightFareID = (int)paramBookingRef.Value;
            }
            catch (SqlException ex_A4)
            {
            }
        }
        public List<Itinerary> Get(string ItineraryID, string SearchID)
        {
            DataTable dataTable = new DataTable();
            List<Itinerary> Itineraries = new List<Itinerary>();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetItinerary", new List<SqlParameter>
				{
					new SqlParameter("@ItineraryID", 1),
					new SqlParameter("@SearchID", SearchID)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    Itineraries.Add(new Itinerary
                    {
                        ItineraryID = dataTable.Rows[index]["ItineraryID"].ToString(),
                        SearchID = dataTable.Rows[index]["SearchID"].ToString(),
                        SearchXML = XTraTech.Helper.Helper.Unzip((byte[])dataTable.Rows[index]["SerializedXML"]),
                        IsBooked = Convert.ToBoolean(dataTable.Rows[index]["IsBooked"])
                    });
                }
            }
            catch (SqlException ex_116)
            {
            }
            return Itineraries;
        }
    }
}
