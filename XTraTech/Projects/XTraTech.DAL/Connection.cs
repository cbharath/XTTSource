using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

namespace XTraTech.DAL
{
    public class Connection
    {
        private static string connectionString;
        public static string ConnectionString
        {
            get
            {
                if (connectionString == null)
                {
                    connectionString = ConfigurationManager.ConnectionStrings["XTraTechConnection"].ConnectionString;
                }
                return connectionString;
            }
        }
    }
}
