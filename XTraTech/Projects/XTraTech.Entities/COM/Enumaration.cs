using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XTraTech.Entities.COM
{
    public class Enumaration
    {
        public enum BookingStatus
        {
            Booked = 1,
            NotBooked,
            Pending,
            Ticketed,
            InProcess
        }
        public enum PaxType
        {
            ADT = 1,
            CHD,
            INF
        }
    }
}
