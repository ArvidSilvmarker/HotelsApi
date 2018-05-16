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
        private DateTime _dateTimeStop = new DateTime(2018, 01, 01);

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

        public HotelFile LatestScandicFile(DateTime fromDate)
        {
            var importPath = _appConfiguration.ImportPath;

            for (var date = fromDate.Date; date > _dateTimeStop; date = date.AddDays(-1))
            {
                string fullPath = $@"{importPath}\Scandic-{date:yyyy-MM-dd}.txt";
                if (File.Exists(fullPath))
                {
                    return new HotelFile {Date = date, Path = fullPath, Exists = true};
                }
            }

            return new HotelFile {Exists = false};
        }

        public List<Hotel> ReadScandicFile()
        {
            var path = _appConfiguration.ImportPath;
            var scandicHotels = new List<Hotel>();
            var hotelFile = LatestScandicFile(DateTime.Now);
        
            if (hotelFile.Exists)
            {
                string[] lines = File.ReadAllLines(hotelFile.Path);
                scandicHotels = MapScandicToHotels(lines);
            }

            return scandicHotels;
        }

        public List<Hotel> MapScandicToHotels(string[] lines)
        {
            var hotels = new List<Hotel>();

            if (lines == null)
                return null;

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

        public HotelFile LatestBestWesternFile(DateTime fromDate)
        {
            var importPath = _appConfiguration.ImportPath;

            for (var date = fromDate.Date; date > _dateTimeStop; date = date.AddDays(-1))
            {
                string fullPath = $@"{importPath}\BestWestern-{date:yyyy-MM-dd}.json";
                if (File.Exists(fullPath))
                {
                    return new HotelFile { Date = date, Path = fullPath, Exists = true };
                }
            }

            return new HotelFile { Exists = false };
        }

        public List<Hotel> ReadBestWesternHotels()
        {

            var bestWesternHotelsList = new List<Hotel>();
            var path = _appConfiguration.ImportPath;
            var hotelFile = LatestBestWesternFile(DateTime.Now);

            if (hotelFile.Exists)
            {
                var json = File.ReadAllText(hotelFile.Path);
                var bestWesternHotel = JsonConvert.DeserializeObject<List<BestWesternHotel>>(json);
                if (bestWesternHotel == null)
                    throw new FormatException("Error in format");
                bestWesternHotelsList = MapBestWesternToHotels(bestWesternHotel);
            }

            return bestWesternHotelsList;
        }

        public List<Hotel> MapBestWesternToHotels(List<BestWesternHotel> bestWesternHotels)
        {
            var hotels = new List<Hotel>();

            if (bestWesternHotels == null)
                return null;

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
