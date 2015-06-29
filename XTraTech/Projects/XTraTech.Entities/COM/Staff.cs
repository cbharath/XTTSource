using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XTraTech.DAL;

namespace XTraTech.Entities.COM
{
    public class Staff
    {
        public int StaffID { get; set; }
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
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

        public void Save(int ClientID)
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@ClientID", this.ClientID));
                sqlParameterList.Add(new SqlParameter("@UserName", UserName));
                sqlParameterList.Add(new SqlParameter("@Password", Password));
                sqlParameterList.Add(new SqlParameter("@CreatedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@ModefiedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@FirstName", FirstName));
                sqlParameterList.Add(new SqlParameter("@LastName", LastName));
                sqlParameterList.Add(new SqlParameter("@Email", Email));
                sqlParameterList.Add(new SqlParameter("@PhoneNumber", PhoneNumber));

                SqlParameter paramBookingRef = new SqlParameter("@StaffID", SqlDbType.Int);
                paramBookingRef.Direction = ParameterDirection.Output;
                sqlParameterList.Add(paramBookingRef);
                SqlHelper.ExecuteNonQuery("InsertStaff", sqlParameterList.ToArray());
                int StaffID = (int)paramBookingRef.Value;
            }
            catch (SqlException ex_167)
            {
            }
        }
        public List<Staff> Load(int ClientID)
        {
            DataTable dataTable = new DataTable();
            List<Staff> Staffs = new List<Staff>();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetStaffs", new List<SqlParameter>
				{
					new SqlParameter("@ClientID", ClientID)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    Staff staff = new Staff();
                    staff.StaffID = dataTable.Rows[index]["StaffID"] != null ? Convert.ToInt32(dataTable.Rows[index]["StaffID"].ToString()) : 0;
                    staff.ClientID = dataTable.Rows[index]["ClientID"] != null ?Convert.ToInt32(dataTable.Rows[index]["ClientID"].ToString()): 0;
                    staff.UserName = dataTable.Rows[index]["UserName"] != null ? dataTable.Rows[index]["UserName"].ToString() : string.Empty;
                    staff.Password = dataTable.Rows[index]["Password"] != null ? dataTable.Rows[index]["Password"].ToString() : string.Empty;
                    staff.FirstName = dataTable.Rows[index]["FirstName"] != null ? dataTable.Rows[index]["FirstName"].ToString() : string.Empty;
                    staff.LastName = dataTable.Rows[index]["LastName"] != null ? dataTable.Rows[index]["LastName"].ToString() : string.Empty;
                    staff.Email = dataTable.Rows[index]["EmailID"] != null ? dataTable.Rows[index]["EmailID"].ToString() : string.Empty;
                    staff.PhoneNumber = dataTable.Rows[index]["PhoneNumber"] != null ? dataTable.Rows[index]["PhoneNumber"].ToString() : string.Empty;
                    staff.CreatedOn = dataTable.Rows[index]["CreatedOn"] != null ? Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]) : DateTime.Now;
                    staff.ModefiedOn = dataTable.Rows[index]["ModefiedOn"] != null ? Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"]) : DateTime.Now;
                    Staffs.Add(staff);
                }
            }
            catch (Exception)
            {
            }
            return Staffs;
        }

        public Staff LoadStaffFromStaffID(int StaffID)
        {
            DataTable dataTable = new DataTable();
            Staff staff = new Staff();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetStaffFromID", new List<SqlParameter>
				{
					new SqlParameter("@StaffID", ClientID)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    staff.StaffID = Convert.ToInt32(dataTable.Rows[index]["StaffID"].ToString());
                    staff.ClientID = Convert.ToInt32(dataTable.Rows[index]["ClintID"].ToString());
                    staff.UserName = dataTable.Rows[index]["UserName"].ToString();
                    staff.Password = dataTable.Rows[index]["Password"].ToString();
                    staff.FirstName = dataTable.Rows[index]["FirstName"].ToString();
                    staff.LastName = dataTable.Rows[index]["LastName"].ToString();
                    staff.Email = dataTable.Rows[index]["Email"].ToString();
                    staff.PhoneNumber = dataTable.Rows[index]["PhoneNumber"].ToString();
                    staff.CreatedOn = Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]);
                    staff.ModefiedOn = Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"]);
                }
            }
            catch (Exception)
            {
            }
            return staff;
        }

        public Staff AuthnticateUser(string UserName, string Password)
        {
            DataTable dataTable = new DataTable();
            Staff staff = null;
            try
            {
                dataTable = SqlHelper.FillDataTableSP("AuthenticateUser", new List<SqlParameter>
				{
					new SqlParameter("@UserName", UserName),
                    new SqlParameter("@Password", Password)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count;)
                {
                    staff = new Staff();
                    staff.StaffID = dataTable.Rows[index]["StaffID"] != null ? Convert.ToInt32(dataTable.Rows[index]["StaffID"].ToString()) : 0;
                    staff.ClientID = dataTable.Rows[index]["ClientID"] != null ? Convert.ToInt32(dataTable.Rows[index]["ClientID"].ToString()) : 0;
                    staff.UserName = dataTable.Rows[index]["UserName"] != null ? dataTable.Rows[index]["UserName"].ToString() : string.Empty;
                    staff.Password = dataTable.Rows[index]["Password"] != null ? dataTable.Rows[index]["Password"].ToString() : string.Empty;
                    staff.FirstName = dataTable.Rows[index]["FirstName"] != null ? dataTable.Rows[index]["FirstName"].ToString() : string.Empty;
                    staff.LastName = dataTable.Rows[index]["LastName"] != null ? dataTable.Rows[index]["LastName"].ToString() : string.Empty;
                    staff.Email = dataTable.Rows[index]["EmailID"] != null ? dataTable.Rows[index]["EmailID"].ToString() : string.Empty;
                    staff.PhoneNumber = dataTable.Rows[index]["PhoneNumber"] != null ? dataTable.Rows[index]["PhoneNumber"].ToString() : string.Empty;
                    staff.CreatedOn = dataTable.Rows[index]["CreatedOn"] != null ? Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]) : DateTime.Now;
                    staff.ModefiedOn = dataTable.Rows[index]["ModefiedOn"] != null ? Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"]) : DateTime.Now;
                    break;
                }
            }
            catch (Exception)
            {
            }
            return staff;
        }
    }
}
