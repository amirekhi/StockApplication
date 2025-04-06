using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Models;
using WebApplication1.Entity;

namespace MyWebApi.Intefaces
{
    
         public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);


        Task<Portfolio> CreateAsync(Portfolio portfolio);

        Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol);

    }
    
}