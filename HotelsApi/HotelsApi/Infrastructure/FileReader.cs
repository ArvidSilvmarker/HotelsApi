using System;
using System.Collections.Generic;
using System.IO;
using HotelsApi.Domain;
using HotelsApi.Domain.Interfaces;

namespace HotelsApi.Infrastructure
{
    public class FileReader : IFileReader
    {
        public List<Hotel> ReadScandicFile()
        {
            var dateTimeStop = new DateTime(2018, 01, 01);

            for (var date = DateTime.Now; date > dateTimeStop; date = date.AddDays(-1))
            {
                if (File.Exists($@"wwwroot\Scandic\Scandic-{date:yyyy-MM-dd}.txt"))
                {
                    string[] lines = File.ReadAllLines($@"wwwroot\Scandic\Scandic-{date:yyyy-MM-dd}.txt");
                    return MapHotels(lines);
                }
               
            }
                
            return null;
        }

        public List<Hotel> MapHotels(string[] lines)
        {
            var hotelList = new List<Hotel>();
            foreach (string line in lines)
            {
                var hotelString = line.Split(',');
                hotelList.Add(new Hotel
                {
                    RegionValue = Convert.ToInt32(hotelString[0]),
                    Name = hotelString[1],
                    RoomsAvailable = Convert.ToInt32(hotelString[2])
                });
            }

            return hotelList;
        }
    }
}
