using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XTraTech.DAL;

namespace XTraTech.Entities.COM
{
    public class Airline
    {
        public string IATA { get; set; }
        public string Name { get; set; }

        public List<Airline> Load()
        {
            DataTable dataTable = new DataTable();
            List<Airline> Airlines = new List<Airline>();
            dataTable = SqlHelper.FillDataTableSP("GetAirlines");
            for (int index = 0; index < dataTable.Rows.Count; index++)
            {
                Airline airline = new Airline();
                airline.IATA = dataTable.Rows[index]["iata"] != null ? dataTable.Rows[index]["iata"].ToString() : string.Empty;
                airline.Name = dataTable.Rows[index]["name"] != null ? dataTable.Rows[index]["name"].ToString() : string.Empty;
                Airlines.Add(airline);
            }
            return Airlines;
        }
    }
}
