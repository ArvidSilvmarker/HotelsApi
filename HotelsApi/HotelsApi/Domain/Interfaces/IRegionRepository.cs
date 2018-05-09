using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsApi.Domain.Interfaces
{
    public interface IRegionRepository
    {
        void CreateRegion(Region region);
        void RemoveRegion(Region region);
        List<Region> ReadAllRegions();
        void ReSeedRegions();
    }
}
