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

        public bool UseDefaultSMTP { get; set; }
        public string SMTPAddress { get; set; }
        public string PortNumber { get; set; }
        public bool EnableSSL { get; set; }
        public string FromEmail { get; set; }
        public string EmailPassword { get; set; }

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

                sqlParameterList.Add(new SqlParameter("@UseDefaultSMTP", UseDefaultSMTP));
                sqlParameterList.Add(new SqlParameter("@SMTPAddress", SMTPAddress));
                sqlParameterList.Add(new SqlParameter("@PortNumber", PortNumber));
                sqlParameterList.Add(new SqlParameter("@EnableSSL", EnableSSL));
                sqlParameterList.Add(new SqlParameter("@FromEmail", FromEmail));
                sqlParameterList.Add(new SqlParameter("@EmailPassword", EmailPassword));

                SqlParameter paramBookingRef = new SqlParameter("@ClientID", SqlDbType.Int);
                paramBookingRef.Direction = ParameterDirection.Output;
                sqlParameterList.Add(paramBookingRef);
                SqlHelper.ExecuteNonQuery("InsertClient", sqlParameterList.ToArray());
                int clientID = (int)paramBookingRef.Value;

                foreach (Staff item in Staffs)
                {
                    item.Save(clientID);
                }
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
                    this.ClintID = dataTable.Rows[index]["ClientID"] != null ? Convert.ToInt32(dataTable.Rows[index]["ClientID"].ToString()) : 0;
                    this.CompanyName = dataTable.Rows[index]["CompanyName"] != null ? dataTable.Rows[index]["CompanyName"].ToString() : string.Empty;
                    this.MemberOf = dataTable.Rows[index]["MemberOf"] != null ? dataTable.Rows[index]["MemberOf"].ToString() : string.Empty;
                    this.Country = dataTable.Rows[index]["Country"] != null ? dataTable.Rows[index]["Country"].ToString() : string.Empty;
                    this.City = dataTable.Rows[index]["City"] != null ? dataTable.Rows[index]["City"].ToString() : string.Empty;
                    this.State = dataTable.Rows[index]["State"] != null ? dataTable.Rows[index]["State"].ToString() : string.Empty;
                    this.ZIP = dataTable.Rows[index]["ZIP"] != null ? dataTable.Rows[index]["ZIP"].ToString() : string.Empty;
                    this.Address1 = dataTable.Rows[index]["Address1"] != null ? dataTable.Rows[index]["Address1"].ToString() : string.Empty;
                    this.Address2 = dataTable.Rows[index]["Address2"] != null ? dataTable.Rows[index]["Address2"].ToString() : string.Empty;
                    this.FirstName = dataTable.Rows[index]["FirstName"] != null ? dataTable.Rows[index]["FirstName"].ToString() : string.Empty;
                    this.LastName = dataTable.Rows[index]["LastName"] != null ? dataTable.Rows[index]["LastName"].ToString() : string.Empty;
                    this.Email = dataTable.Rows[index]["Email"] != null ? dataTable.Rows[index]["Email"].ToString() : string.Empty;
                    this.PhoneNumber = dataTable.Rows[index]["PhoneNumber"] != null ? dataTable.Rows[index]["PhoneNumber"].ToString() : string.Empty;

                    this.UseDefaultSMTP = dataTable.Rows[index]["UseDefaultSMTP"] != null ? Convert.ToBoolean(dataTable.Rows[index]["UseDefaultSMTP"]) : true;
                    this.SMTPAddress = dataTable.Rows[index]["SMTPAddress"] != null ? dataTable.Rows[index]["SMTPAddress"].ToString() : string.Empty;
                    this.PortNumber = dataTable.Rows[index]["PortNumber"] != null ? dataTable.Rows[index]["PortNumber"].ToString() : string.Empty;
                    this.EnableSSL = dataTable.Rows[index]["EnableSSL"] != null ? Convert.ToBoolean(dataTable.Rows[index]["EnableSSL"]) : false;
                    this.FromEmail = dataTable.Rows[index]["FromEmail"] != null ? dataTable.Rows[index]["FromEmail"].ToString() : string.Empty;
                    this.EmailPassword = dataTable.Rows[index]["EmailPassword"] != null ? dataTable.Rows[index]["EmailPassword"].ToString() : string.Empty;

                    this.CreatedOn = dataTable.Rows[index]["ClientID"] != null ? Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]) : DateTime.Now;
                    this.ModefiedOn = dataTable.Rows[index]["ClientID"] != null ? Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"]) : DateTime.Now;
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

        public List<Client> Load()
        {
            DataTable dataTable = new DataTable();
            List<Client> clients = new List<Client>();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetFullClient");
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    Client singleClient = new Client();
                    singleClient.ClintID = dataTable.Rows[index]["ClientID"] != null ? Convert.ToInt32(dataTable.Rows[index]["ClientID"].ToString()) : 0;
                    singleClient.CompanyName = dataTable.Rows[index]["CompanyName"] != null ? dataTable.Rows[index]["CompanyName"].ToString() : string.Empty;
                    singleClient.MemberOf = dataTable.Rows[index]["MemberOf"] != null ? dataTable.Rows[index]["MemberOf"].ToString() : string.Empty;
                    singleClient.Country = dataTable.Rows[index]["Country"] != null ? dataTable.Rows[index]["Country"].ToString() : string.Empty;
                    singleClient.City = dataTable.Rows[index]["City"] != null ? dataTable.Rows[index]["City"].ToString() : string.Empty;
                    singleClient.State = dataTable.Rows[index]["State"] != null ? dataTable.Rows[index]["State"].ToString() : string.Empty;
                    singleClient.ZIP = dataTable.Rows[index]["ZIP"] != null ? dataTable.Rows[index]["ZIP"].ToString() : string.Empty;
                    singleClient.Address1 = dataTable.Rows[index]["Address1"] != null ? dataTable.Rows[index]["Address1"].ToString() : string.Empty;
                    singleClient.Address2 = dataTable.Rows[index]["Address2"] != null ? dataTable.Rows[index]["Address2"].ToString() : string.Empty;
                    singleClient.FirstName = dataTable.Rows[index]["FirstName"] != null ? dataTable.Rows[index]["FirstName"].ToString() : string.Empty;
                    singleClient.LastName = dataTable.Rows[index]["LastName"] != null ? dataTable.Rows[index]["LastName"].ToString() : string.Empty;
                    singleClient.Email = dataTable.Rows[index]["Email"] != null ? dataTable.Rows[index]["Email"].ToString() : string.Empty;
                    singleClient.PhoneNumber = dataTable.Rows[index]["PhoneNumber"] != null ? dataTable.Rows[index]["PhoneNumber"].ToString() : string.Empty;

                    singleClient.UseDefaultSMTP = dataTable.Rows[index]["UseDefaultSMTP"] != null ? Convert.ToBoolean(dataTable.Rows[index]["UseDefaultSMTP"]) : true;
                    singleClient.SMTPAddress = dataTable.Rows[index]["SMTPAddress"] != null ? dataTable.Rows[index]["SMTPAddress"].ToString() : string.Empty;
                    singleClient.PortNumber = dataTable.Rows[index]["PortNumber"] != null ? dataTable.Rows[index]["PortNumber"].ToString() : string.Empty;
                    singleClient.EnableSSL = dataTable.Rows[index]["EnableSSL"] != null ? Convert.ToBoolean(dataTable.Rows[index]["EnableSSL"]) : false;
                    singleClient.FromEmail = dataTable.Rows[index]["FromEmail"] != null ? dataTable.Rows[index]["FromEmail"].ToString() : string.Empty;
                    singleClient.EmailPassword = dataTable.Rows[index]["EmailPassword"] != null ? dataTable.Rows[index]["EmailPassword"].ToString() : string.Empty;

                    singleClient.CreatedOn = dataTable.Rows[index]["CreatedOn"] != null ? Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]) : DateTime.Now;
                    singleClient.ModefiedOn = dataTable.Rows[index]["ModefiedOn"] != null ? Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"]) : DateTime.Now;

                    clients.Add(singleClient);
                }
                
            }
            catch (Exception)
            {
            }
            return clients;
        }
    }
}
