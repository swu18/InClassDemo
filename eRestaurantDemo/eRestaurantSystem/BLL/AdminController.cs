using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addtional Namespaces

using eRestaurantSystem.DAL;
using eRestaurantSystem.DAL.Entities;
using eRestaurantSystem.DAL.DTOs;
using eRestaurantSystem.DAL.POCOs;
using System.ComponentModel;// Object Data Source


#endregion


namespace eRestaurantSystem.BLL
{
    [DataObject]
    public class AdminController
    {

        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SpecialEvent> SpecialEvents_List()
        {
            //connect to our DbContext class in the DAL
            //create an instance of the class
            //constructor => connection name
            // we will use a transaxtion to hold our query 
            using (var context = new eRestaurantContext()) //context is variable name , new is instance name
            {

                // option1: method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList(); //.SpecialEvents is DbSet()

                //option2:query syntax
                var results = from item in context.SpecialEvents
                              orderby item.Description
                              select item;
                return results.ToList();


            }



        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Waiter> Waiter_List()
        {
            //connect to our DbContext class in the DAL
            //create an instance of the class
            //constructor => connection name
            // we will use a transaxtion to hold our query 
            using (var context = new eRestaurantContext()) //context is variable name , new is instance name
            {

                // option1: method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList(); //.SpecialEvents is DbSet()

                //option2:query syntax
                var results = from item in context.Waiters
                              orderby item.LastName,item.FirstName
                              select item;
                return results.ToList(); // none , 1 or more rows.


            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Waiter GetWaiterByID(int waiterid)
        {
           
            using (var context = new eRestaurantContext()) //context is variable name , new is instance name
            {

                // option1: method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList(); //.SpecialEvents is DbSet()

                //option2:query syntax
                var results = from item in context.Waiters
                              where item.WaiterID == waiterid
                              select item;
                return results.FirstOrDefault();// one row at most


            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Reservation> GetReservationByEventCode(string eventcode) //variable always lower case!!
        {
            using (var context = new eRestaurantContext()) //context is variable name , new is instance name
            {

                //option2:query syntax
                var results = from item in context.Reservations
                              where item.EventCode.Equals(eventcode)
                              orderby item.CustomerName, item.ReservationDate
                              select item;
                return results.ToList();
            }

        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]

        public List<ReservationsByDate> GetReservationByDate(String reservationdate)// since reservationbydate is created by our own, has to put into name space
        {

            using (var context = new eRestaurantContext())
            {
                //Linq is not very playful or cooperative with 
                //DateTime
                //extract the year, moth and day ourseleves out
                //of the passed parameter value

                int theYear = (DateTime.Parse(reservationdate)).Year;
                int theMonth = (DateTime.Parse(reservationdate)).Month;
                int theDay = (DateTime.Parse(reservationdate)).Day;

                var results = from eventitem in context.SpecialEvents
                              orderby eventitem.Description
                              select new ReservationsByDate()//ReservationByDate is optional) a new instance for each specialevent row on the table 
                              {
                                  Description = eventitem.Description,
                                  Reservation = from row in eventitem.Reservations
                                                where row.ReservationDate.Year == theYear
                                                && row.ReservationDate.Month == theMonth
                                                && row.ReservationDate.Day == theDay
                                                select new ReservationDetail()// a new for each reservation of the particular specialevent 
                                                {
                                                    CustomerName = row.CustomerName,
                                                    ReservationDate = row.ReservationDate,
                                                    NumberInParty = row.NumberInParty,
                                                    ContactPhone = row.ContactPhone,
                                                    ReservationStatus = row.ReservationStatus
                                                }

                              };
                return results.ToList();


            }


        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryItems> MenuCategoryItems_List()
        {
            using (var context = new eRestaurantContext())
            {
                var result = from menuitem in context.MenuCategories
                             orderby menuitem.Description
                             select new MenuCategoryItems()
                             {
                                 Description = menuitem.Description,
                                 MenuItems = from row in menuitem.MenuItems
                                             select new MenuItem()
                                               {
                                                   Description = row.Description,
                                                   Price = row.CurrentPrice,
                                                   Calories = row.Calories,
                                                   Comment = row.Comment
                                               }
                             };
                return result.ToList();
            }
        }

        #endregion

        #region Add, Update,Delete of CRUD for CQRS(Comment query responsibility )
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void SpecialEvent_Add(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {

                // these methods are execute using an instance level item
                // set up a instance pointer and initialize to null
                SpecialEvent added = null;
                // set up commanc to execute the add
                added = context.SpecialEvents.Add(item);
                //command is not executed until it it actually saved.
                context.SaveChanges();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void SpecialEvent_Update(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {

                // indicate the updateing item instance 
                //alter the Modified Status flag for this instance
                context.Entry<SpecialEvent>(context.SpecialEvents.Attach(item)).State =
               System.Data.Entity.EntityState.Modified;
                //command is not executed until it it actually saved.
                context.SaveChanges();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void SpecialEvent_Delete(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {

                // indicate the updateing item instance 
                //alter the Modified Status flag for this instance
                SpecialEvent existing = context.SpecialEvents.Find(item.EventCode);
                // set up the command to execute the delete
                context.SpecialEvents.Remove(existing);
                //command is not executed until it it actually saved.
                context.SaveChanges();
            }
        }

        // copy from last 
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Waiters_Add(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {

                // these methods are execute using an instance level item
                // set up a instance pointer and initialize to null
                Waiter added = null;
                // set up commanc to execute the add
                added = context.Waiters.Add(item);
                //command is not executed until it it actually saved.
                context.SaveChanges();
               // the Waiter instance added contains the newly inserted
               // record to sql including the generated play value
                return added.WaiterID;
            
            }
        }


        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Waiters_Update(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {

                // indicate the updateing item instance 
                //alter the Modified Status flag for this instance
                context.Entry<Waiter>(context.Waiters.Attach(item)).State =
               System.Data.Entity.EntityState.Modified;
                //command is not executed until it it actually saved.
                context.SaveChanges();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Waiters_Delete(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {

                // indicate the updateing item instance 
                //alter the Modified Status flag for this instance
                Waiter existing = context.Waiters.Find(item.WaiterID);
                // set up the command to execute the delete
                context.Waiters.Remove(existing);
                //command is not executed until it it actually saved.
                context.SaveChanges();
            }
        }


        #endregion
    }

}// eof namespace

