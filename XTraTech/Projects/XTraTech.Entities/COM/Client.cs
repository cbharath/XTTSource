using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XTraTech.DAL;

namespace XTraTech.Entities.COM
{
    public class Client
    {
        public int ClintID { get; set; }
        public string CompanyName { get; set; }
        public string MemberOf { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public void Save()
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@CompanyName", this.CompanyName));
                sqlParameterList.Add(new SqlParameter("@MemberOf", this.MemberOf));
                sqlParameterList.Add(new SqlParameter("@Country", this.Country));
                sqlParameterList.Add(new SqlParameter("@City", this.City));
                sqlParameterList.Add(new SqlParameter("@State", this.State));
                sqlParameterList.Add(new SqlParameter("@ZIP", this.ZIP));
                sqlParameterList.Add(new SqlParameter("@Address1", this.Address1));
                sqlParameterList.Add(new SqlParameter("@Address2", this.Address2));
                sqlParameterList.Add(new SqlParameter("@CreatedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@ModefiedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@FirstName", FirstName));
                sqlParameterList.Add(new SqlParameter("@LastName", LastName));
                sqlParameterList.Add(new SqlParameter("@Email", Email));
                sqlParameterList.Add(new SqlParameter("@PhoneNumber", PhoneNumber));

                SqlParameter paramBookingRef = new SqlParameter("@ClientID", SqlDbType.Int);
                paramBookingRef.Direction = ParameterDirection.Output;
                sqlParameterList.Add(paramBookingRef);
                SqlHelper.ExecuteNonQuery("InsertClient", sqlParameterList.ToArray());
                int flightFareID = (int)paramBookingRef.Value;
            }
            catch (SqlException ex_167)
            {
            }
        }
        public List<FlightFare> Load(int FlightDetailsID)
        {
            DataTable dataTable = new DataTable();
            List<FlightFare> FlightFares = new List<FlightFare>();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetFlightFares", new List<SqlParameter>
				{
					new SqlParameter("@FlightDetailsID", FlightDetailsID)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    FlightFares.Add(new FlightFare
                    {
                        FlightFareID = Convert.ToInt32(dataTable.Rows[index]["FlightFareID"].ToString()),
                        PaxCount = Convert.ToInt32(dataTable.Rows[index]["PaxCount"].ToString()),
                        Currency = dataTable.Rows[index]["Currency"].ToString(),
                        FareType = dataTable.Rows[index]["FareType"].ToString(),
                        BaseFare = Convert.ToDecimal(dataTable.Rows[index]["BaseFare"]),
                        Tax = Convert.ToDecimal(dataTable.Rows[index]["Tax"]),
                        MarkUp = Convert.ToDecimal(dataTable.Rows[index]["MarkUp"]),
                        ROE = Convert.ToDecimal(dataTable.Rows[index]["ROE"]),
                        PaxType = (XTraTech.Entities.COM.Enumaration.PaxType)Convert.ToInt32(dataTable.Rows[index]["PaxType"]),
                        CreatedOn = Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]),
                        ModefiedOn = Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"])
                    });
                }
            }
            catch (Exception)
            {
            }
            return FlightFares;
        }
    }
}
