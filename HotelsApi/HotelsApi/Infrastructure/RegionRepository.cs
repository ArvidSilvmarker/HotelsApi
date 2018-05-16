using System;
using System.Linq;
using System.Collections.Generic;
using HotelsApi.Domain;
using HotelsApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;


namespace HotelsApi.Infrastructure
{
    public class RegionRepository : IRegionRepository
    {
        private readonly HotelContext _context;

        public RegionRepository(HotelContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
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
            foreach (var region in new Seed().SeedRegions)
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

        public bool IsDatabaseUp() =>
            (_context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();

        public bool IsDatabaseRunning()
        {
            try
            {
                _context.Database.OpenConnection();
                _context.Database.CloseConnection();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
