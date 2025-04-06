using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Models;

namespace MyWebApi.Intefaces
{
    public interface ITokenService
    {
         string? CreateToken(AppUser appUser);
    }
}