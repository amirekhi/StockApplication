using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;
using WebApplication1.Entity;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        public DbSet<Stock> Stocks { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Portfolio> Portfolios {set; get;}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Portfolio>(x => x.HasKey(p => new {p.AppUserId , p.StockId} ));

            builder.Entity<Portfolio>()
            .HasOne(x => x.AppUser)
            .WithMany(x => x.Portfolios)
            .HasForeignKey(x => x.AppUserId);


            builder.Entity<Portfolio>()
            .HasOne(x => x.Stock)
            .WithMany(x => x.Portfolios)
            .HasForeignKey(x => x.StockId);

            List<IdentityRole> roles = new List<IdentityRole> 
            {
                
                 new IdentityRole
                {
                    Id = "d60c5b44-34e7-49ef-a0c5-763a98b6e74f", // Static GUID
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "d60c5b44-34e7-49ef-a0c5-763a98b6e74f" // Optional, but useful
                },
                new IdentityRole
                {
                    Id = "bb2b129d-9ae2-4c99-9fcd-7f72f774a2d2", // Static GUID
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "bb2b129d-9ae2-4c99-9fcd-7f72f774a2d2" // Optional
                }                      
            };

            builder.Entity<IdentityRole>().HasData(roles);

       }
    }
}
