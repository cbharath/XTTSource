using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XTraTech.DAL;
using System.Data;
using System.Data.SqlClient;

namespace XTraTech.Entities.COM
{
    public class Airport
    {
        private string _airportCode;
        private string _airportName;
        private string _cityCode;
        private string _cityName;
        private string _countryName;
        private string _countryCode;
        private string _LangCode;
        private string _ICAO;
        private float _latitude;
        private float _longitude;
        private float _altitude;
        private float _timezone;
        private string _DST;
        private string _UTCOffset;
        public string airportCode
        {
            get
            {
                return this._airportCode;
            }
            set
            {
                this._airportCode = value;
            }
        }
        public string airportName
        {
            get
            {
                return this._airportName;
            }
            set
            {
                this._airportName = value;
            }
        }
        public string cityCode
        {
            get
            {
                return this._cityCode;
            }
            set
            {
                this._cityCode = value;
            }
        }
        public string cityName
        {
            get
            {
                return this._cityName;
            }
            set
            {
                this._cityName = value;
            }
        }
        public string countryName
        {
            get
            {
                return this._countryName;
            }
            set
            {
                this._countryName = value;
            }
        }
        public string countryCode
        {
            get
            {
                return this._countryCode;
            }
            set
            {
                this._countryCode = value;
            }
        }
        public string LangCode
        {
            get
            {
                return this._LangCode;
            }
            set
            {
                this._LangCode = value;
            }
        }
        public string ICAO
        {
            get
            {
                return this._ICAO;
            }
            set
            {
                this._ICAO = value;
            }
        }
        public float latitude
        {
            get
            {
                return this._latitude;
            }
            set
            {
                this._latitude = value;
            }
        }
        public float longitude
        {
            get
            {
                return this._longitude;
            }
            set
            {
                this._longitude = value;
            }
        }
        public float altitude
        {
            get
            {
                return this._altitude;
            }
            set
            {
                this._altitude = value;
            }
        }
        public float timezone
        {
            get
            {
                return this._timezone;
            }
            set
            {
                this._timezone = value;
            }
        }
        public string DST
        {
            get
            {
                return this._DST;
            }
            set
            {
                this._DST = value;
            }
        }
        public string UTCOffset
        {
            get
            {
                return this._UTCOffset;
            }
            set
            {
                this._UTCOffset = value;
            }
        }
        public List<Airport> Load(string LangCode)
        {
            DataTable dataTable = new DataTable();
            List<Airport> Airports = new List<Airport>();
            List<Airport> result;
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetAirports", new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", LangCode)
                }.ToArray());
                foreach (DataRow item in dataTable.Rows)
                {
                    Airport Airport = new Airport();
                    Airport.airportCode = item["airportCode"].ToString();
                    Airport.airportName = item["airportName"].ToString();
                    if (item["altitude"] != null)
                    {
                        float floatTemp;
                        float.TryParse(item["altitude"].ToString(), out floatTemp);
                        Airport.altitude = floatTemp;
                    }
                    Airport.cityCode = item["cityCode"].ToString();
                    Airport.cityName = item["cityName"].ToString();
                    Airport.countryCode = item["countryCode"].ToString();
                    Airport.countryName = item["countryName"].ToString();
                    Airport.DST = item["DST"].ToString();
                    Airport.ICAO = item["ICAO"].ToString();
                    Airport.LangCode = item["LangCode"].ToString();
                    if (item["latitude"] != null)
                    {
                        float floatTemp;
                        float.TryParse(item["latitude"].ToString(), out floatTemp);
                        Airport.altitude = floatTemp;
                    }
                    if (item["longitude"] != null)
                    {
                        float floatTemp;
                        float.TryParse(item["longitude"].ToString(), out floatTemp);
                        Airport.altitude = floatTemp;
                    }
                    if (item["timezone"] != null)
                    {
                        float floatTemp;
                        float.TryParse(item["timezone"].ToString(), out floatTemp);
                        Airport.altitude = floatTemp;
                    }
                    this.UTCOffset = item["UTCOffset"].ToString();
                    Airports.Add(Airport);
                }
                result = Airports;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
