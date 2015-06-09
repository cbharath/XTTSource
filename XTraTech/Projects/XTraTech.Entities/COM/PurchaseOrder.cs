using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using XTraTech.Helper;
using XTraTech.DAL;
namespace XTraTech.Entities.COM
{
    public class PurchaseOrder
    {
        public string PurchaseOrderID
        {
            get;
            set;
        }
        public XTraTech.Entities.COM.Enumaration.BookingStatus Status
        {
            get;
            set;
        }
        public int clientID
        {
            get;
            set;
        }
        public string SearchID
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
        public List<FlightDetail> FlightDetails
        {
            get;
            set;
        }
        public List<PassengerDetail> PassengerDetails
        {
            get;
            set;
        }
        public string PrimaryPax
        {
            get;
            set;
        }
        public string PaxCountSummary
        {
            get;
            set;
        }
        public string TravelDate
        {
            get;
            set;
        }
        public string Airports
        {
            get;
            set;
        }
        public string PaymentStatus
        {
            get;
            set;
        }
        public string Total
        {
            get;
            set;
        }
        public int Save()
        {
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@Status", this.Status));
                sqlParameterList.Add(new SqlParameter("@clientID", this.clientID));
                sqlParameterList.Add(new SqlParameter("@SearchID", this.SearchID));
                sqlParameterList.Add(new SqlParameter("@CreatedOn", DateTime.UtcNow));
                sqlParameterList.Add(new SqlParameter("@ModefiedOn", DateTime.UtcNow));
                SqlParameter paramBookingRef = new SqlParameter("@PurchaseOrderID", SqlDbType.Int);
                paramBookingRef.Direction = ParameterDirection.Output;
                sqlParameterList.Add(paramBookingRef);
                SqlHelper.ExecuteNonQuery("InsertPurchaseOrder", sqlParameterList.ToArray());
                int bookingRef = (int)paramBookingRef.Value;
                this.PurchaseOrderID = XTraTech.Helper.Helper.CreatePurchaseReference(bookingRef, DateTime.UtcNow);
                foreach (FlightDetail item in this.FlightDetails)
                {
                    FlightDetail flightDetail = new FlightDetail();
                    flightDetail = item;
                    flightDetail.Save(bookingRef);
                }
                foreach (PassengerDetail item2 in this.PassengerDetails)
                {
                    PassengerDetail passengerDetail = new PassengerDetail();
                    passengerDetail = item2;
                    passengerDetail.Save(bookingRef);
                }
            }
            catch (SqlException ex_175)
            {
                return 0;
            }
            return 0;
        }
        public void Load(bool DoLoadFlightDetails, bool DoLoadPassengerDetails)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = SqlHelper.FillDataTableSP("GetPurchaseOrder", new List<SqlParameter>
				{
					new SqlParameter("@PurchaseOrderID", this.PurchaseOrderID)
				}.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    int purchaseOrderID = Convert.ToInt32(dataTable.Rows[index]["PurchaseOrderID"].ToString());
                    this.Status = (XTraTech.Entities.COM.Enumaration.BookingStatus)Convert.ToInt32(dataTable.Rows[index]["Status"]);
                    this.SearchID = dataTable.Rows[index]["SearchID"].ToString();
                    this.clientID = Convert.ToInt32(dataTable.Rows[index]["clientID"].ToString());
                    this.CreatedOn = Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]);
                    this.ModefiedOn = Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"]);
                    this.PurchaseOrderID = XTraTech.Helper.Helper.CreatePurchaseReference(purchaseOrderID, this.CreatedOn);
                    if (DoLoadFlightDetails)
                    {
                        FlightDetail flightDetail = new FlightDetail();
                        this.FlightDetails = flightDetail.Load(purchaseOrderID, true, true);
                    }
                    if (DoLoadPassengerDetails)
                    {
                        PassengerDetail passengerDetail = new PassengerDetail();
                        this.PassengerDetails = passengerDetail.Load(purchaseOrderID);
                    }
                }
            }
            catch (SqlException ex_186)
            {
            }
        }
        public List<PurchaseOrder> LoadAll()
        {
            DataTable dataTable = new DataTable();
            List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
            try
            {
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                dataTable = SqlHelper.FillDataTableSP("GetAllPurchaseOrder", sqlParameterList.ToArray());
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    PurchaseOrder purOrder = new PurchaseOrder();
                    int purchaseOrderID = Convert.ToInt32(dataTable.Rows[index]["PurchaseOrderID"].ToString());
                    purOrder.Status = (XTraTech.Entities.COM.Enumaration.BookingStatus)Convert.ToInt32(dataTable.Rows[index]["Status"]);
                    purOrder.SearchID = dataTable.Rows[index]["SearchID"].ToString();
                    purOrder.clientID = Convert.ToInt32(dataTable.Rows[index]["clientID"].ToString());
                    purOrder.CreatedOn = Convert.ToDateTime(dataTable.Rows[index]["CreatedOn"]);
                    purOrder.ModefiedOn = Convert.ToDateTime(dataTable.Rows[index]["ModefiedOn"]);
                    purOrder.PurchaseOrderID = XTraTech.Helper.Helper.CreatePurchaseReference(purchaseOrderID, purOrder.CreatedOn);
                    FlightDetail flightDetail = new FlightDetail();
                    purOrder.FlightDetails = flightDetail.Load(purchaseOrderID, true, true);
                    PassengerDetail passengerDetail = new PassengerDetail();
                    purOrder.PassengerDetails = passengerDetail.Load(purchaseOrderID);
                    purOrder.TravelDate = purOrder.FlightDetails.FirstOrDefault<FlightDetail>().FlightSegments.FirstOrDefault<FlightSegment>().DepartureDateTime.ToShortDateString();
                    purOrder.PrimaryPax = purOrder.PassengerDetails.FirstOrDefault<PassengerDetail>().FirstName + purOrder.PassengerDetails.FirstOrDefault<PassengerDetail>().LastName;
                    int adultcount = purOrder.PassengerDetails.Count((PassengerDetail c) => c.PaxType == XTraTech.Entities.COM.Enumaration.PaxType.ADT);
                    int childtcount = purOrder.PassengerDetails.Count((PassengerDetail c) => c.PaxType == XTraTech.Entities.COM.Enumaration.PaxType.CHD);
                    int infanttcount = purOrder.PassengerDetails.Count((PassengerDetail c) => c.PaxType == XTraTech.Entities.COM.Enumaration.PaxType.INF);
                    purOrder.PaxCountSummary = string.Concat(new object[]
					{
						adultcount,
						"A,",
						childtcount,
						"C,",
						infanttcount,
						"I"
					});
                    string airportsummary = string.Empty;
                    foreach (FlightSegment item in
                        from s in purOrder.FlightDetails.FirstOrDefault<FlightDetail>().FlightSegments
                        where !s.IsReturn
                        select s)
                    {
                        airportsummary = airportsummary + item.Origin + "," + item.Destination;
                    }
                    purOrder.Airports = airportsummary;
                    purOrder.PaymentStatus = "Deposit";
                    purOrder.Total = Helper.GetFare(purOrder.FlightDetails.FirstOrDefault<FlightDetail>().FlightFares, "total");
                    purchaseOrderList.Add(purOrder);
                }
            }
            catch (SqlException ex_379)
            {
            }
            return purchaseOrderList;
        }
    }
}
