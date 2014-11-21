using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XTraTech.DAL;
namespace XTraTech.Entities.COM
{
    public class FlightFare
    {
        public int FlightFareID
        {
            get;
            set;
        }
        public string Currency
        {
            get;
            set;
        }
        public decimal BaseFare
        {
            get;
            set;
        }
        public decimal Tax
        {
            get;
            set;
        }
        public decimal MarkUp
        {
            get;
            set;
        }
        public decimal ROE
        {
            get;
            set;
        }
        public int PaxCount
        {
            get;
            set;
        }
        public XTraTech.Entities.COM.Enumaration.PaxType PaxType
        {
            get;
            set;
        }
        public string FareType
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
                sqlParameterList.Add(new SqlParameter("@Currency", this.Currency));
                sqlParameterList.Add(new SqlParameter("@BaseFare", this.BaseFare));
                sqlParameterList.Add(new SqlParameter("@Tax", this.Tax));
                sqlParameterList.Add(new SqlParameter("@MarkUp", this.MarkUp));
                sqlParameterList.Add(new SqlParameter("@ROE", this.ROE));
                sqlParameterList.Add(new SqlParameter("@PaxCount", this.PaxCount));
                sqlParameterList.Add(new SqlParameter("@PaxType", this.PaxType));
                sqlParameterList.Add(new SqlParameter("@FareType", this.FareType));
                sqlParameterList.Add(new SqlParameter("@CreatedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@ModefiedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@FlightDetailsID", FlightDetailID));
                SqlParameter paramBookingRef = new SqlParameter("@FlightFareID", SqlDbType.Int);
                paramBookingRef.Direction = ParameterDirection.Output;
                sqlParameterList.Add(paramBookingRef);
                SqlHelper.ExecuteNonQuery("InsertFlightFare", sqlParameterList.ToArray());
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
