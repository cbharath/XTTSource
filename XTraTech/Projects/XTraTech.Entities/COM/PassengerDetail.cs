using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XTraTech.DAL;
namespace XTraTech.Entities.COM
{
    public class PassengerDetail
    {
        public int PassengerDetailID
        {
            get;
            set;
        }
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
        public string Email
        {
            get;
            set;
        }
        public DateTime? DOB
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }
        public string AreaCode
        {
            get;
            set;
        }
        public string CountryCode
        {
            get;
            set;
        }
        public string FFNumber
        {
            get;
            set;
        }
        public XTraTech.Entities.COM.Enumaration.PaxType PaxType
        {
            get;
            set;
        }
        public string Mobile
        {
            get;
            set;
        }
        public string PassportNumber
        {
            get;
            set;
        }
        public string PassportNationality
        {
            get;
            set;
        }
        public DateTime PassportDOE
        {
            get;
            set;
        }
        public string SeatPref
        {
            get;
            set;
        }
        public string MealPref
        {
            get;
            set;
        }
        public string Address1
        {
            get;
            set;
        }
        public string Address2
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
        public void Save(int PurchaseOrderID)
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@Title", this.Title));
                sqlParameterList.Add(new SqlParameter("@FirstName", this.FirstName));
                sqlParameterList.Add(new SqlParameter("@LastName", this.LastName));
                sqlParameterList.Add(new SqlParameter("@Email", this.Email));
                sqlParameterList.Add(new SqlParameter("@DOB", this.DOB));
                sqlParameterList.Add(new SqlParameter("@Gender", this.Gender));
                sqlParameterList.Add(new SqlParameter("@AreaCode", this.AreaCode));
                sqlParameterList.Add(new SqlParameter("@CountryCode", this.CountryCode));
                sqlParameterList.Add(new SqlParameter("@FFNumber", this.FFNumber));
                sqlParameterList.Add(new SqlParameter("@PaxType", this.PaxType));
                sqlParameterList.Add(new SqlParameter("@Mobile", this.Mobile));
                sqlParameterList.Add(new SqlParameter("@PassportNumber", this.PassportNumber));
                sqlParameterList.Add(new SqlParameter("@PassportNationality", this.PassportNationality));
                sqlParameterList.Add(new SqlParameter("@PassportDOE", this.PassportDOE));
                sqlParameterList.Add(new SqlParameter("@SearPref", 1));
                sqlParameterList.Add(new SqlParameter("@MealPref", 1));
                sqlParameterList.Add(new SqlParameter("@Address1", this.Address1));
                sqlParameterList.Add(new SqlParameter("@Address2", this.Address2));
                sqlParameterList.Add(new SqlParameter("@CreatedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@ModefiedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@PurchaseOrderID", PurchaseOrderID));
                SqlParameter paramBookingRef = new SqlParameter("@PaxID", SqlDbType.Int);
                paramBookingRef.Direction = ParameterDirection.Output;
                sqlParameterList.Add(paramBookingRef);
                SqlHelper.ExecuteNonQuery("InsertFlightPassenger", sqlParameterList.ToArray());
                int flightFareID = (int)paramBookingRef.Value;
            }
            catch (SqlException ex_23E)
            {
            }
        }
        public List<PassengerDetail> Load(int PurchaseOrderID)
        {
            DataTable dataTable = new DataTable();
            List<PassengerDetail> PassengerDetails = new List<PassengerDetail>();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetFlightPassengers", new List<SqlParameter>
				{
					new SqlParameter("@PurchaseOrderID", PurchaseOrderID)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    PassengerDetails.Add(new PassengerDetail
                    {
                        PassengerDetailID = Convert.ToInt32(dataTable.Rows[index]["PaxID"]),
                        Title = dataTable.Rows[index]["Title"].ToString(),
                        FirstName = dataTable.Rows[index]["FirstName"].ToString(),
                        LastName = dataTable.Rows[index]["LastName"].ToString(),
                        Email = dataTable.Rows[index]["Email"].ToString(),
                        DOB = new DateTime?(Convert.ToDateTime(dataTable.Rows[index]["DOB"])),
                        PassportDOE = Convert.ToDateTime(dataTable.Rows[index]["PassportDOE"]),
                        Gender = dataTable.Rows[index]["Gender"].ToString(),
                        AreaCode = dataTable.Rows[index]["AreaCode"].ToString(),
                        CountryCode = dataTable.Rows[index]["CountryCode"].ToString(),
                        FFNumber = dataTable.Rows[index]["FFNumber"].ToString(),
                        Mobile = dataTable.Rows[index]["Mobile"].ToString(),
                        PassportNumber = dataTable.Rows[index]["PassportNumber"].ToString(),
                        PassportNationality = dataTable.Rows[index]["PassportNationality"].ToString(),
                        SeatPref = dataTable.Rows[index]["SeatPref"].ToString(),
                        MealPref = dataTable.Rows[index]["MealPref"].ToString(),
                        Address1 = dataTable.Rows[index]["Address1"].ToString(),
                        Address2 = dataTable.Rows[index]["Address2"].ToString(),
                        PaxType = (XTraTech.Entities.COM.Enumaration.PaxType)Convert.ToInt32(dataTable.Rows[index]["PaxType"]),
                        CreatedOn = Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]),
                        ModefiedOn = Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"])
                    });
                }
            }
            catch (Exception)
            {
            }
            return PassengerDetails;
        }
    }
}
