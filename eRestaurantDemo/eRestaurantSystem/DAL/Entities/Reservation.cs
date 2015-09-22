﻿using System;
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
    public class Reservation
    {
        [Key] // this is option, as you have real PK below
        public int ReservationID { get; set; }
        public string CustomerName{ get; set; }
        public DateTime reservationData{ get; set; }
        public int NumberInParty{ get; set; }
        public string ContactPhone{ get; set; }
        public string ReservationStatus{ get; set; }
        public string EventCode { get; set; }

        //Navigation properties
        public virtual SpecialEvent Event { get; set; }

        //the Reservations table (sql) is a many to many relation to the Tables table(sql)
        //Sql solves this problem by having an associage table that has a compound primary key created from 
        //Reservations and Tables.

        //We will NOT be creating an entity for this associate table.
        // Instead we will create on overlaoad map in out DbContext class
        //However, we can still create the virtual navigation propery to 
        //sxxmondate this relationship 

        public virtual ICollection<Table> Tables { get; set; } 



    }
}