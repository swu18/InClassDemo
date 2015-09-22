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
    class SpecialEvent

     {
        [Key]
        public string EventCode { get; set; }
        public string Description {get;set;}
        public bool  Active {get;set;}

        //Navigational virtual properties
        //this is a parent to the Reservation entity
        public virtual ICollection<Reservation> Reservations { get; set; } //since this parent table


    }
}
