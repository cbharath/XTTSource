using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XTraTech.DAL;
using System.Data.SqlClient;

namespace XTraTech.Helper
{
    public class Configuration
    {
        public static Dictionary<string, string> XTraTechConfigurationSettings = new Dictionary<string, string>();

        public static void ReloadStatics()
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetConfigurations");
                XTraTechConfigurationSettings.Clear();
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    XTraTechConfigurationSettings.Add(dataTable.Rows[index]["ConfigName"].ToString(), dataTable.Rows[index]["ConfigValue"].ToString());
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
