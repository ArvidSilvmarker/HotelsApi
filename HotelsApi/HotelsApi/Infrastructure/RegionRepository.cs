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
            foreach (var region in new Seed().SeedRegions())
            {
                CreateRegion(region);
            }
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
