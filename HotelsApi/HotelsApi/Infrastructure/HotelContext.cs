using Microsoft.EntityFrameworkCore;

public class HotelContext : DbContext
{
    public DbSet<Region> Region { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = (localdb)\\mssqllocaldb; Database = db-hotels; Trusted_Connection = True; ");
    }
}