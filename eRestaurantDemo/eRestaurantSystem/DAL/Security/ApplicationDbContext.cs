using eRestaurantSystem.DAL.Entities.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurantSystem.DAL.Security
{
    public class ApplicationDbContext : 
        IdentityDbContext<ApplicationUser> //Second DAL
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}
