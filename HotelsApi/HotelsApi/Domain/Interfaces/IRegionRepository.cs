using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsApi.Domain.Interfaces
{
    public interface IRegionRepository
    {
        void CreateRegion(Region region);
        Region ReadRegion(int id);
        List<Region> ReadAllRegions();
        void UpdateRegion(Region region);
        void DeleteRegion(int id);
        void ReSeedRegions();
        bool IsDatabaseRunning();
    }
}
