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
    public class Table
    {
        [Key]

        public int TableID { get; set; }
        public byte TableNumber{get;set;}
        public bool Smoking {get;set;}
        public int Capacity {get;set;}
        public bool Available {get;set;}


        //Navigation properties
        //the Reservations table (sql) is a many to many relation to the Tables table(sql)
        //Sql solves this problem by having an associage table that has a compound primary key created from 
        //Reservations and Tables.

        //We will NOT be creating an entity for this associate table.
        // Instead we will create on overlaoad map in out DbContext class
        //However, we can still create the virtual navigation propery to 
        //sxxmondate this relationship

        public virtual ICollection<Reservation> Reservations { get; set; }



    }
}
