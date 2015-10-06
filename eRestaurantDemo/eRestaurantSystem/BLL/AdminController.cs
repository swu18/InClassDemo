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
        public List<Reservation> GetReservationByEventCode(string eventcode) //variable always lower case!!
        {
            using (var context = new eRestaurantContext()) //context is variable name , new is instance name
            {

               //option2:query syntax
                var results = from item in context.Reservations
                              where item.EventCode.Equals(eventcode)
                              orderby item.CustomerName,item.ReservationDate
                              select item;
                return results.ToList();
            }

        }
            [DataObjectMethod(DataObjectMethodType.Select,false)]
            
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



    }


}

