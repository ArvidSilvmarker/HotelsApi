using HotelsApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Infrastructure
{
    public class HotelContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }

        public HotelContext(DbContextOptions<HotelContext> context) : base(context)
        {
        }
    }

}
