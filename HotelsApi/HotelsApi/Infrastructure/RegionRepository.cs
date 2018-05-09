using System;
using System.Collections.Generic;
using System.Linq;
using HotelsApi.Domain;
using HotelsApi.Domain.Interfaces;

namespace HotelsApi.Infrastructure
{
    public class RegionRepository : IRegionRepository
    {
        private readonly HotelContext context;
        private Seed seed;

        public RegionRepository()
        {
            context = new HotelContext();
            seed = new Seed();
        }

        public void CreateRegion(Region region)
        {
            context.Regions.Add(region);
            context.SaveChanges();
        }
        public Region ReadRegion(int id)
        {
            return context.Regions.FirstOrDefault(r => r.Id == id);
        }

        public List<Region> ReadAllRegions()
        {
            return context.Regions.ToList();
        }

        public void UpdateRegion(Region region)
        {
            context.Regions.Update(region);
            context.SaveChanges();
        }

        public void DeleteRegion(int id)
        {
            var delete = context.Regions.First(r => r.Id == id);
            context.Regions.Remove(delete);
            context.SaveChanges();
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
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
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
