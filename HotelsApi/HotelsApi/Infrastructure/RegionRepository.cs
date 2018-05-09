using System;
using System.Collections.Generic;
using System.Linq;
using HotelsApi.Domain;
using HotelsApi.Domain.Interfaces;

namespace HotelsApi.Infrastructure
{
    public class RegionRepository : IRegionRepository
    {
        private readonly HotelContext context = new HotelContext();
        public void CreateRegion(Region region)
        {
            context.Regions.Add(region);
            context.SaveChanges();
        }

        public void RemoveRegion(Region region)
        {
            context.Regions.Remove(region);
            context.SaveChanges();
        }

        public List<Region> ReadAllRegions()
        {
            return context.Regions.ToList();
        }

        public void ReSeedRegions()
        {
            ClearAll();
            foreach (var region in new Seed().SeedRegions())
            {
                CreateRegion(region);
            }
        }

        private void ClearAll()
        {
            context.Database.EnsureCreated();
            context.Database.EnsureDeleted();
        }
    }
}
