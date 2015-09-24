﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Addtional Namespaces

using eRestaurantSystem.DAL;
using eRestaurantSystem.DAL.Entities;
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
                   return context.SpecialEvents.OrderBy(x => x.Description).ToList(); //.SpecialEvents is DbSet()

              
               }
          

        
        }

    }
}
