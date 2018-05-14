using HotelsApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Infrastructure
{
    public class HotelContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        "Server = (localdb)\\mssqllocaldb; Database = db-hotels; Trusted_Connection = True; ");
        //}

        public HotelContext(DbContextOptions<HotelContext> context) : base(context)
        {
        }
    }

}
