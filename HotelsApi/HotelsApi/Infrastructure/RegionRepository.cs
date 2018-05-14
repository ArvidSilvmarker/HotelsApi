using System.Linq;
using System.Collections.Generic;
using HotelsApi.Domain;
using HotelsApi.Domain.Interfaces;


namespace HotelsApi.Infrastructure
{
    public class RegionRepository : IRegionRepository
    {
        private readonly HotelContext _context;
        private Seed seed;

        public RegionRepository(HotelContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            seed = new Seed();
        }

        public void CreateRegion(Region region)
        {
            _context.Regions.Add(region);
            _context.SaveChanges();
        }
        public Region ReadRegion(int id)
        {
            return _context.Regions.FirstOrDefault(r => r.Id == id);
        }

        public List<Region> ReadAllRegions()
        {
            return _context.Regions.ToList();
        }

        public void UpdateRegion(Region region)
        {
            _context.Regions.Update(region);
            _context.SaveChanges();
        }

        public void DeleteRegion(int id)
        {
            var delete = _context.Regions.First(r => r.Id == id);
            _context.Regions.Remove(delete);
            _context.SaveChanges();
        }

        public void ReSeedRegions()
        {
            ClearAll();
            //RecreateDatabase();
            seed = new Seed();
            foreach (var region in seed.SeedRegions)
            {
                CreateRegion(region);
            }
        }

        public void RecreateDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        private void ClearAll()
        {
            foreach (var region in ReadAllRegions())
            { 
                DeleteRegion(region.Id);
            }
        }
    }
}
