using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsApi.Domain.Interfaces
{
    public interface IFileReader
    {
        List<Hotel> ReadAllHotels();
        HotelFile GetLatestScandicFile(DateTime fromDate);
        HotelFile GetLatestBestWesternFile(DateTime fromDate);
    }
}
