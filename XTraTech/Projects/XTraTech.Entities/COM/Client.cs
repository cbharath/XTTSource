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
        public List<Staff> Staffs { get; set; }
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
        public void Load(int ClinetID, bool doLoadStaff = true)
        {
            DataTable dataTable = new DataTable();
            List<Client> clients = new List<Client>();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetClient", new List<SqlParameter>
				{
					new SqlParameter("@ClientID", ClinetID)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    this.ClintID = Convert.ToInt32(dataTable.Rows[index]["ClintID"].ToString());
                    this.CompanyName = dataTable.Rows[index]["CompanyName"].ToString();
                    this.MemberOf = dataTable.Rows[index]["MemberOf"].ToString();
                    this.Country = dataTable.Rows[index]["Country"].ToString();
                    this.City = dataTable.Rows[index]["City"].ToString();
                    this.State = dataTable.Rows[index]["State"].ToString();
                    this.ZIP = dataTable.Rows[index]["ZIP"].ToString();
                    this.Address1 = dataTable.Rows[index]["Address1"].ToString();
                    this.Address2 = dataTable.Rows[index]["Address2"].ToString();
                    this.FirstName = dataTable.Rows[index]["FirstName"].ToString();
                    this.LastName = dataTable.Rows[index]["LastName"].ToString();
                    this.Email = dataTable.Rows[index]["Email"].ToString();
                    this.PhoneNumber = dataTable.Rows[index]["PhoneNumber"].ToString();

                    this.CreatedOn = Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]);
                    this.ModefiedOn = Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"]);
                    if (doLoadStaff)
                    {
                        Staff staff = new Staff();
                        this.Staffs = staff.Load(ClintID);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
