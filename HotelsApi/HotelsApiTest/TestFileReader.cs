using System;
using System.IO;
using System.Linq;
using HotelsApi;
using HotelsApi.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelsApiTest
{
    [TestClass]
    public class TestFileReader
    {
        private FileReader fille;

        [TestInitialize]
        public void Init()
        {
            fille = new FileReader(new AppConfiguration { ImportPath = "C:\\Hotels" });
        }

        [TestMethod]
        public void ParseScandicRubinen()
        {
            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt"))
            {
                writetext.WriteLine("50,Scandic Rubinen,15");
            }

            var hotelList = fille.ReadScandicFile();
            var hotel = hotelList[0];
            Assert.AreEqual(50, hotel.RegionValue);
            Assert.AreEqual("Scandic Rubinen", hotel.Name);
            Assert.AreEqual(15, hotel.RoomsAvailable);
        }


        [TestMethod]
        public void ParseBestWestern()
        {
            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\BestWestern-{DateTime.Now:yyyy-MM-dd}.json"))
            {
                writetext.WriteLine("[{\"Reg\": 50, \"Name\": \"Hotell Eggers\", \"LedigaRum\": 100}]");
            }
            var hotelList = fille.ReadBestWesternHotels();
            var hotel = hotelList[0];
            Assert.AreEqual(50, hotel.RegionValue);
            Assert.AreEqual("Hotell Eggers", hotel.Name);
            Assert.AreEqual(100, hotel.RoomsAvailable);
        }


        [TestMethod]
        public void FormatExceptionScandic()
        {
            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt"))
            {
                writetext.WriteLine("");
            }
            Assert.ThrowsException<FormatException>(() => fille.ReadScandicFile());
        }

        [TestMethod]
        public void FormatExceptionBestWestern()
        {
            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\BestWestern-{DateTime.Now:yyyy-MM-dd}.json"))
            {
                writetext.WriteLine("");
            }
            Assert.ThrowsException<FormatException>(() => fille.ReadBestWesternHotels());
        }

        [TestMethod]
        public void SelectCorrectDateScandic()
        {
            File.Delete($@"C:\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt");

            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\Scandic-{DateTime.Now.AddDays(-1):yyyy-MM-dd}.txt"))
            {
                writetext.WriteLine("50,Scandic Rubinen,15");
            }

            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\Scandic-{DateTime.Now.AddDays(1):yyyy-MM-dd}.txt"))
            {
                writetext.WriteLine("60,Scandic Blåbär,20");
            }

            var hotelList = fille.ReadScandicFile();
            var hotel = hotelList[0];
            Assert.AreEqual(50, hotel.RegionValue);
            Assert.AreEqual("Scandic Rubinen", hotel.Name);
            Assert.AreEqual(15, hotel.RoomsAvailable);
        }

        [TestMethod]
        public void SelectCorrectDateBestWestern()
        {
            File.Delete($@"C:\Hotels\BestWestern-{DateTime.Now:yyyy-MM-dd}.json");

            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\BestWestern-{DateTime.Now.AddDays(-1):yyyy-MM-dd}.json"))
            {
                writetext.WriteLine("[{\"Reg\": 50, \"Name\": \"Hotell Eggers\", \"LedigaRum\": 100}]");
            }

            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\BestWestern-{DateTime.Now.AddDays(1):yyyy-MM-dd}.json"))
            {
                writetext.WriteLine("[{\"Reg\": 60, \"Name\": \"Blåbär\", \"LedigaRum\": 0}]");
            }

            var hotelList = fille.ReadBestWesternHotels();
            var hotel = hotelList[0];
            Assert.AreEqual(50, hotel.RegionValue);
            Assert.AreEqual("Hotell Eggers", hotel.Name);
            Assert.AreEqual(100, hotel.RoomsAvailable);
        }
    }
}
