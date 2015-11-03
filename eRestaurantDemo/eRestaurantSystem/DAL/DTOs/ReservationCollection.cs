using eRestaurantSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurantSystem.DAL.DTOs
{
    public class ReservationCollection
    {

        public int Hour { get; set; }
        public TimeSpan SeatingTime { get { return new TimeSpan(Hour, 0, 0); } }//timespan is only read property
        public virtual ICollection<ReservationSummary> Reservations { get; set; }
    }
}
