using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eRestaurantSystem.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

#endregion

namespace eRestaurantSystem.DAL
{
 
    //1.this class should only accessable from classess inside 
    //this component library
    //2.therefore the access level for this class will be inernal 
    //3. this class will inherit from DbContext (EntityFramework)
    internal class eRestaurantContext:DbContext
    {

        // create a constructor which will pass the connection string 
        // name to the DbContext

        public eRestaurantContext() : base("name = EatIn")  // connect string name
        { 
                 
        }

        //Set up of mapping DbSet<T> property
        //Map an entity to a database table 

        public DbSet<SpecialEvent> SpecialEvents { get; set; } // this is property name
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Waiter> Waiters { get; set; }

        //when overriding the OnModelCreating(), it is important to remember to call the base method's implementation
        //before you exit the method 
        // the ManyToManyNavigationPropertyConfiguration.Map method
        //lets you configure the tables and columns used for this many to many relationship.

        // it takes a ManyToManyNavigationPropertyConfiguration instance in which you specify the column names 
        //by calling the MapLeftKey, MapRisghtKey, and ToTable methods

        // the "left" key is the one specified in the HasMany method
        // the "right" key is the one specified in the WithMany method

        // this navigation replaces the sql associated table that breaks up 
        // many to many relationship
        // this technique should ONLY be used if the associate table in 
        // sql has ONLY a compound primary key and NO non-key attribute

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            modelBuilder
                .Entity<Reservation>().HasMany(r => r.Tables)
                .WithMany(t => t.Reservations)
                .Map(mapping =>

                    {
                     mapping.ToTable("ReservationTables");
                     mapping.MapLeftKey("ReservationID");
                     mapping.MapRightKey("TableID");
                          
                    
                     
                    });


            base.OnModelCreating(modelBuilder);
        }

        



    }
}
