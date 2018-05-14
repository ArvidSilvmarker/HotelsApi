using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HotelsApi.Domain;
using HotelsApi.Domain.Interfaces;
using Newtonsoft.Json;

namespace HotelsApi.Infrastructure
{
    public class FileReader : IFileReader
    {
        public List<Hotel> ReadScandicFile()
        {
            var dateTimeStop = new DateTime(2018, 01, 01);

            for (var date = DateTime.Now; date > dateTimeStop; date = date.AddDays(-1))
            {
                if (File.Exists($@"wwwroot\Hotels\Scandic-{date:yyyy-MM-dd}.txt"))
                {
                    string[] lines = File.ReadAllLines($@"wwwroot\Hotels\Scandic-{date:yyyy-MM-dd}.txt");
                    return MapScandicToHotels(lines);
                }
               
            }
                
            return null;
        }

        public List<Hotel> ReadAllHotels()
        {
            var allHotels = ReadScandicFile();
            allHotels.AddRange(ReadBestWesternHotels());

            return allHotels;
        }

        public List<Hotel> MapScandicToHotels(string[] lines)
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


        public List<Hotel> ReadBestWesternHotels()
        {
            var dateTimeStop = new DateTime(2018, 01, 01);

            for (var date = DateTime.Now; date > dateTimeStop; date = date.AddDays(-1))
            {
                if (File.Exists($@"wwwroot\Hotels\BestWestern-{date:yyyy-MM-dd}.json"))
                {
                    var bestWesternHotel = JsonConvert.DeserializeObject<List<BestWesternHotel>>(File.ReadAllText($@"wwwroot\Hotels\BestWestern-{date:yyyy-MM-dd}.json"));
                    return MapBestWesternToHotels(bestWesternHotel);
                }

            }

            return null;
        }

        private List<Hotel> MapBestWesternToHotels(List<BestWesternHotel> bestWesternHotels)
        {
            var hotels = new List<Hotel>();
            foreach (var bestWesternHotel in bestWesternHotels)
            {
                hotels.Add(new Hotel{
                    RegionValue = bestWesternHotel.Reg,
                    Name = bestWesternHotel.Name,
                    RoomsAvailable = bestWesternHotel.LedigaRum
                });

            }

            return hotels;
        }
    }
}
