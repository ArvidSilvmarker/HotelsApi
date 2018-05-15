using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HotelsApi.Common;
using HotelsApi.Domain;
using HotelsApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace HotelsApi.Infrastructure
{
    public class FileReader : IFileReader
    {
        private AppConfiguration _appConfiguration;
        public FileReader(AppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public List<Hotel> ReadAllHotels()
        {
            var allHotels = ReadScandicFile();
            allHotels.AddRange(ReadBestWesternHotels());

            return allHotels;
        }

        public List<Hotel> ReadScandicFile()
        {
            var dateTimeStop = new DateTime(2018, 01, 01);
            var path = _appConfiguration.ImportPath;
            var scandicHotels = new List<Hotel>();

            for (var date = DateTime.Now; date > dateTimeStop; date = date.AddDays(-1))
            {
                if (File.Exists($@"{path}\Scandic-{date:yyyy-MM-dd}.txt"))
                {
                    string[] lines = File.ReadAllLines($@"{path}\Scandic-{date:yyyy-MM-dd}.txt");
                    scandicHotels = MapScandicToHotels(lines);
                    break;
                }
            }

            return scandicHotels;
        }

        public List<Hotel> MapScandicToHotels(string[] lines)
        {
            var hotels = new List<Hotel>();
            foreach (string line in lines)
            {
                var hotelString = line.Split(',');
                var hotel = new Hotel
                {
                    RegionValue = Convert.ToInt32(hotelString[0].Trim()),
                    Name = hotelString[1].Trim(),
                    RoomsAvailable = Convert.ToInt32(hotelString[2].Trim())
                };
                if (HotelValidation.ValidateHotel(hotel))
                    hotels.Add(hotel);
                else
                    throw new FormatException("Error in format.");
            }

            return hotels;
        }


        public List<Hotel> ReadBestWesternHotels()
        {
            var dateTimeStop = new DateTime(2018, 01, 01);
            var bestWesternHotelsList = new List<Hotel>();
            var path = _appConfiguration.ImportPath;

            for (var date = DateTime.Now; date > dateTimeStop; date = date.AddDays(-1))
            {
                if (File.Exists($@"{path}\BestWestern-{date:yyyy-MM-dd}.json"))
                {
                    var json = File.ReadAllText($@"{path}\BestWestern-{date:yyyy-MM-dd}.json");
                    var bestWesternHotel = JsonConvert.DeserializeObject<List<BestWesternHotel>>(json);
                    if (bestWesternHotel == null)
                        throw new FormatException("Error in format");
                    bestWesternHotelsList = MapBestWesternToHotels(bestWesternHotel);
                    break;
                }
            }

            return bestWesternHotelsList;
        }

        public List<Hotel> MapBestWesternToHotels(List<BestWesternHotel> bestWesternHotels)
        {
            var hotels = new List<Hotel>();
            foreach (var bestWesternHotel in bestWesternHotels)
            {
                var hotel = new Hotel
                {
                    RegionValue = bestWesternHotel.Reg,
                    Name = bestWesternHotel.Name,
                    RoomsAvailable = bestWesternHotel.LedigaRum
                };
                if (HotelValidation.ValidateHotel(hotel))
                    hotels.Add(hotel);
                else
                    throw new ArgumentException("Error in format.");
            }

            return hotels;
        }
    }
}
