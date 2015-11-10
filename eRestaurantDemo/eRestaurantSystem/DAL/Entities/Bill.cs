using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//#region Addtional Namespaces

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

//#endregion

namespace eRestaurantSystem.DAL.Entities
{
    public class Bill
    {
        public int BillID { get; set; }
        public DateTime BillDate { get; set; }
        public TimeSpan? OrderPlaced { get; set; }//DateTime?
        public TimeSpan? OrderReady { get; set; }// new add 20151109
        public TimeSpan? OrderServed { get; set; }//new add
        public TimeSpan? OrderPaid { get; set; }//new add
        public int NumberInParty { get; set; }
        public bool PaidStatus { get; set; }
        public int WaiterID { get; set; }
        public int? TableID { get; set; }
        public int? ReservationID { get; set; }
        //public bool OrderReady { get; set; } is not in table anymore
        public string Comment { get; set; }

        // Navigation Properties
        public virtual ICollection<BillItem> Items { get; set; }
        public virtual Waiter Waiter { get; set; }
        public virtual Table Table { get; set; }
        public virtual Reservation Reservation { get; set; }

        public Bill() // set default value
        {
            BillDate = DateTime.Now;
        }
    }
}
