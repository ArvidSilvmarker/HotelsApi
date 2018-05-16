﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsApi.Domain.Interfaces
{
    public interface IFileReader
    {
        List<Hotel> ReadAllHotels();
        HotelFile LatestScandicFile(DateTime fromDate);
        HotelFile LatestBestWesternFile(DateTime fromDate);
    }
}
