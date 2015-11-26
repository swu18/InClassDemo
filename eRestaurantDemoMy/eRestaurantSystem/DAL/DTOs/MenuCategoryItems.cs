using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespace
#endregion

namespace eRestaurantSystem.DAL.DTOs
{
    public class MenuCategoryItems
    {
        public string Description {get;set;}
        public IEnumerable MenuItems{get;set;} //IEnumberable => for a list of data



    }
}
