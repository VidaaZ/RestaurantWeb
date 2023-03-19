using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class ApplicationUser:IdentityUser
        //we create this model to extend Identity  table columns of users
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
