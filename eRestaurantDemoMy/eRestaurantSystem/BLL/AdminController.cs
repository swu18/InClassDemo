﻿using System;
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
using System.Data.Entity;// help with Datetime and Timespan Linq concern


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

       

        [DataObjectMethod(DataObjectMethodType.Select,false)]// for ODS
        public List<WaiterBilling>GetWaiterBillReport()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {

                var results = from abillrow in context.Bills//context? 1. put context
                              where abillrow.BillDate.Month == 5
                              orderby abillrow.BillDate,
                                      abillrow.Waiter.LastName,
                                      abillrow.Waiter.FirstName
                              select new WaiterBilling() //3.put class name
                              {
                                  BillDate =  abillrow.BillDate.Year +"/"+
                                                 abillrow.BillDate.Month +"/"+
                                                 abillrow.BillDate.Day,
                                  WaiterName = abillrow.Waiter.LastName + ", " +
                                               abillrow.Waiter.FirstName,
                                  BillID = abillrow.BillID,
                                  BillTotal = abillrow.Items.Sum(eachbillitemrow =>  //billitem=>item?
                                            eachbillitemrow.Quantity * eachbillitemrow.SalePrice),
                                  PartySize = abillrow.NumberInParty,
                                  Contact = abillrow.Reservation.CustomerName
                              }; //2. put ;

             return results.ToList();
            
            }
        
        
        }

        #endregion

        #region Add, Update,Delete of CRUD for CQRS(Command Query Responsibility Segregation )
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

        //Query for report
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CategoryMenuItems> GetReportCategoryMenuItems()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var results = from cat in context.Items
                              orderby cat.Category.Description, cat.Description// MenuCategory?
                              select new CategoryMenuItems
                              {
                                  CategoryDescription = cat.Category.Description,
                                  ItemDescription = cat.Description,
                                  Price = cat.CurrentPrice,
                                  Calories = cat.Calories,
                                  Comment = cat.Comment
                              };

                return results.ToList(); // this was .Dump() in Linqpad
            }
        }


        #endregion

        #region FrontDesk

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public DateTime GetLastBillDateTime()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var result = context.Bills.Max(eachBillrow => eachBillrow.BillDate);
                return result;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ReservationCollection> ReservationsByTime(DateTime date)
        {
            using (var context = new eRestaurantContext())
            {
                var result = (from data in context.Reservations
                              where data.ReservationDate.Year == date.Year
                              && data.ReservationDate.Month == date.Month
                              && data.ReservationDate.Day == date.Day
                                  // && data.ReservationDate.Hour == timeSlot.Hours
                              && data.ReservationStatus == Reservation.Booked
                              select new ReservationSummary()
                              {
                                  ID = data.ReservationID,
                                  Name = data.CustomerName,
                                  Date = data.ReservationDate,
                                  NumberInParty = data.NumberInParty,
                                  Status = data.ReservationStatus,
                                  Event = data.Event.Description,
                                  Contact = data.ContactPhone
                              }).ToList();
                //the second part of this method uses the results of the
                //first linq query.
                //Linq to Entity will only execute the query when it deems
                //necessary for having the results in memory.
                //
                //to get your query to execute and have the resulting data
                //inside memory for further use, you can attach the .ToList()
                //to the previous query

                //note: the second query is NOT using an Entity
                //it is using the results from a previous query

                //itemGroup is a temporary in memory data collection
                //this collection can be used in selecting your final
                //data collection.
                var finalResult = from item in result
                                  orderby item.NumberInParty
                                  group item by item.Date.Hour into itemGroup
                                  select new ReservationCollection()
                                  {
                                      Hour = itemGroup.Key,
                                      Reservations = itemGroup.ToList()
                                  };
                return finalResult.OrderBy(x => x.Hour).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public bool ReservationsForToday(DateTime date)
        {
            return ReservationsByTime(date).Count > 0 ? true : false;  //?
        
        
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SeatingSummary> SeatingByDateTime(DateTime date, TimeSpan newtime)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var step1 = from data in context.Tables
                            select new
                            {
                                Table = data.TableNumber,
                                Seating = data.Capacity,
                                // This sub-query gets the bills for walk-in customers
                                WalkIns = from walkIn in data.Bills
                                          where
                                                 walkIn.BillDate.Year == date.Year
                                              && walkIn.BillDate.Month == date.Month
                                              && walkIn.BillDate.Day == date.Day
                                              //remember link to entity does not play nicley with DateTime/TimeSpan
                                              //ofwhich time of day belongs

                                           //   && walkIn.BillDate.TimeOfDay <= newtime
                                              //inside System.Data.Entity is a class of functions
                                              //that will help with DateTime/Timespan concerns
                                            && DbFunctions.CreateTime(walkIn.BillDate.Hour,
                                          walkIn.BillDate.Minute, walkIn.BillDate.Second) <= newtime

                                              && (!walkIn.OrderPaid.HasValue || walkIn.OrderPaid.Value >= newtime)
                                          //                          && (!walkIn.PaidStatus || walkIn.OrderPaid >= time)
                                          select walkIn,
                                // This sub-query gets the bills for reservations
                                Reservations = from booking in data.Reservations
                                               from reservationParty in booking.Bills
                                               where
                                                      reservationParty.BillDate.Year == date.Year
                                                   && reservationParty.BillDate.Month == date.Month
                                                   && reservationParty.BillDate.Day == date.Day
                                                   //  && reservationParty.BillDate.TimeOfDay <= newtime
                                                && DbFunctions.CreateTime(reservationParty.BillDate.Hour,
                                          reservationParty.BillDate.Minute,
                                          reservationParty.BillDate.Second) <= newtime

                                                   && (!reservationParty.OrderPaid.HasValue || reservationParty.OrderPaid.Value >= newtime)
                                               //                          && (!reservationParty.PaidStatus || reservationParty.OrderPaid >= time)
                                               select reservationParty
                            };


                // Step 2 - Union the walk-in bills and the reservation bills while extracting the relevant bill info
                // .ToList() helps resolve the "Types in Union or Concat are constructed incompatibly" error
                var step2 = from data in step1.ToList() // .ToList() forces the first result set to be in memory
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                CommonBilling = from info in data.WalkIns.Union(data.Reservations)
                                                select new // info
                                                {
                                                    BillID = info.BillID,
                                                    BillTotal = info.Items.Sum(bi => bi.Quantity * bi.SalePrice),
                                                    Waiter = info.Waiter.FirstName,
                                                    Reservation = info.Reservation
                                                }
                            };


                // Step 3 - Get just the first CommonBilling item
                //         (presumes no overlaps can occur - i.e., two groups at the same table at the same time)
                var step3 = from data in step2.ToList()
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.CommonBilling.Count() > 0,
                                // .FirstOrDefault() is effectively "flattening" my collection of 1 item into a 
                                // single object whose properties I can get in step 4 using the dot (.) operator
                                CommonBilling = data.CommonBilling.FirstOrDefault()
                            };




                //step3.Dump();
                // Step 4 - Build our intended seating summary info
                var step4 = from data in step3
                            select new SeatingSummary() // the POCO class to use in my BLL
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.Taken,
                                // use a ternary expression to conditionally get the bill id (if it exists)
                                BillID = data.Taken ?               // if(data.Taken)
                                         data.CommonBilling.BillID  // value to use if true
                                       : (int?)null,               // value to use if false
                                BillTotal = data.Taken ?
                                            data.CommonBilling.BillTotal : (decimal?)null,
                                Waiter = data.Taken ? data.CommonBilling.Waiter : (string)null,
                                ReservationName = data.Taken ?
                                                  (data.CommonBilling.Reservation != null ?
                                                   data.CommonBilling.Reservation.CustomerName : (string)null)
                                                : (string)null
                            };
                return step4.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<WaiterOnDuty> ListWaiters()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var result = from person in context.Waiters
                             where person.ReleaseDate == null
                             select new WaiterOnDuty()
                             {
                                 WaiterId = person.WaiterID,
                                 FullName = person.FirstName + " " + person.LastName
                             };
                return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]//20151116 for linkbutton
        public List<SeatingSummary> AvailableSeatingByDateTime(DateTime date,TimeSpan time)

        {
            var results = from seats in SeatingByDateTime(date, time)
                          where !seats.Taken
                          select seats;
          return results.ToList();
                         
        }

        public void SeatCustomer(DateTime when, byte tablenumber,
                                 int numberinparty, int waiterid)
        {

            //business logic checking should be done
            //BEFORE attempting to place data on the database
            //rule1: is the seat available
            //rule2: is the selected table capicity sufficicent
            // get the available seats 

            var availableseatrs = AvailableSeatingByDateTime(when.Date,
                                                             when.TimeOfDay);
            // start my transaction
            using (eRestaurantContext context = new eRestaurantContext())
            {

                //create a holding list for possible  business logic
                //this is need for the MessageUserControl
                List<string> errors = new List<string>();

                if (!availableseatrs.Exists(foreachseat => foreachseat.Table == tablenumber))
                {

                    // the table numberis not available 
                    errors.Add("Table is currently not availabe");


                }
                else if (!availableseatrs.Exists(foreachseat => foreachseat.Table == tablenumber
                         && foreachseat.Seating >= numberinparty))
                {

                    //the table is available but not large enough
                    errors.Add("Insufficient seating capacity for number of customers");

                }
                //check if any errors to business rules exist
                if (errors.Count > 0)
                {
                    //throw an exception which will terminate the transaction
                    //BusinessRuleException is part of the MessageUserControl setup
                    throw new BusinessRuleException("unable to seat customer", errors);
                    //assume your data is valid
                    //create an instance of the Bill entity and fill with data
                    Bill seatedcustomer = new Bill();
                    seatedcustomer.BillDate = when;
                    seatedcustomer.NumberInParty = numberinparty;
                    seatedcustomer.WaiterID = waiterid;
                    seatedcustomer.TableID = tablenumber;

                    //issue the command to add a record to the bill entity
                    context.Bills.Add(seatedcustomer);
                    //save sand commin the changes to the entity
                    context.SaveChanges();
                }

            }

        }
       
       

        public void SeatCustomer(DateTime when, int reservationId, List<byte> tables, int waiterId)
        {
            var availableSeats = AvailableSeatingByDateTime(when.Date, when.TimeOfDay);
            using (var context = new RestaurantContext())
            {
                List<string> errors = new List<string>();
                // Rule checking:
                // - Reservation must be in Booked status
                // - Table must be available - typically a direct check on the table, but proxied based on the mocked time here
                // - Table must be big enough for the # of customers
                var reservation = context.Reservations.Find(reservationId);
                if (reservation == null)
                    errors.Add("The specified reservation does not exist");
                else if (reservation != null && reservation.ReservationStatus != Reservation.Booked)
                    errors.Add("The reservation's status is not valid for seating. Only booked reservations can be seated.");
                var capacity = 0;
                foreach (var tableNumber in tables)
                {
                    if (!availableSeats.Exists(x => x.Table == tableNumber))
                        errors.Add("Table " + tableNumber + " is currently not available");
                    else
                        capacity += availableSeats.Single(x => x.Table == tableNumber).Seating;
                }
                if (capacity < reservation.NumberInParty)
                    errors.Add("Insufficient seating capacity for number of customers. Alternate tables must be used.");
                if (errors.Count > 0)
                    throw new BusinessRuleException("Unable to seat customer", errors);
                // 1) Create a blank bill with assigned waiter
                Bill seatedCustomer = new Bill()
                {
                    BillDate = when,
                    NumberInParty = reservation.NumberInParty,
                    WaiterID = waiterId,
                    ReservationID = reservation.ReservationID
                };
                context.Bills.Add(seatedCustomer);
                // 2) Add the tables for the reservation and change the reservation's status to arrived
                foreach (var tableNumber in tables)
                    reservation.Tables.Add(context.Tables.Single(x => x.TableNumber == tableNumber));
                reservation.ReservationStatus = Reservation.Arrived;
                var updatable = context.Entry(context.Reservations.Attach(reservation));
                updatable.Property(x => x.ReservationStatus).IsModified = true;
                //updatable.Reference(x=>x.Tables).
                // 3) Save changes
                context.SaveChanges();
            }
            //string message = String.Format("Not yet implemented. Need to seat reservation {0} for waiter {1} at tables {2}", reservationId, waiterId, string.Join(", ", tables));
            //throw new NotImplementedException(message);
        }


        #endregion

    }//eof class

}// eof namespace

