using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelsApi.Domain.Interfaces;
using HotelsApi.Infrastructure;

namespace HotelsApi.Domain
{
    public class HotelService
    {
        private IFileReader _fileReader;
        private IRegionRepository _regionRepository;

        public HotelService(IRegionRepository regionRepository, AppConfiguration appConfiguration)
        {
            _regionRepository = regionRepository;
            _fileReader = new FileReader(appConfiguration);
        }

        public List<Region> GetAllRegionsWithHotels()
        {
            var hotelList = _fileReader.ReadAllHotels();
            var listOfRegions = _regionRepository.ReadAllRegions();
            foreach (var region in listOfRegions)
            {
                foreach (var hotel in hotelList)
                {
                    if (region.Value == hotel.RegionValue)
                    {
                        region.Hotels.Add(hotel);
                    }
                }
            }

            return listOfRegions;
        }
    }
}
