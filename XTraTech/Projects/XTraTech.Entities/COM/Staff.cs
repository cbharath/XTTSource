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
                    Staffs.Add(staff);
                }
            }
            catch (Exception)
            {
            }
            return Staffs;
        }
    }
}
