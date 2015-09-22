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
    public class SpecialEvent

     {
        [Key]
        [Required(ErrorMessage="An Event Code is required (only one character)")]//() error message
        [StringLength(1,ErrorMessage= "Event Code is only one character in length")]
        public string EventCode { get; set; }
        [Required (ErrorMessage= "Description is required field.")]
        [StringLength(30,ErrorMessage="Description has maximum length of 30 characters") ]
        public string Description {get;set;}

        public bool  Active {get;set;}

        //Navigational virtual properties
        //this is a parent to the Reservation entity
        public virtual ICollection<Reservation> Reservations { get; set; } //since this parent table

        //default value can ge set in the class constructor
        public SpecialEvent()
        {
            Active = true;
        
        }

    }
}
