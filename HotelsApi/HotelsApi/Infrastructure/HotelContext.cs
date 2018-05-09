using HotelsApi.Domain;
using Microsoft.EntityFrameworkCore;

public class HotelContext : DbContext
{
    public DbSet<Region> Regions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = (localdb)\\mssqllocaldb; Database = db-hotels; Trusted_Connection = True; ");
    }
}