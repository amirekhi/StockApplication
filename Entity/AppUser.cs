using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Entity;

namespace MyWebApi.Models
{
    public class AppUser : IdentityUser
    {
       public List<Portfolio> Portfolios {get; set;} = new List<Portfolio>();
    }
}