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
        private FileReader fille = new FileReader(new AppConfiguration{ImportPath = "C:\\Hotels"});

        [TestInitialize]
        public void Init()
        {
            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt"))
            {
                writetext.WriteLine("50,Scandic Rubinen,15");
            }

            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\BestWestern-{DateTime.Now:yyyy-MM-dd}.json"))
            {
                writetext.WriteLine("[{\"Reg\": 50, \"Name\": \"Hotell Eggers\", \"LedigaRum\": 100}]");
            }
        }

        [TestMethod]
        public void ParseScandicRubinen()
        {
            var hotelList = fille.ReadScandicFile();
            var hotel = hotelList[0];
            Assert.AreEqual(50, hotel.RegionValue);
            Assert.AreEqual("Scandic Rubinen", hotel.Name);
            Assert.AreEqual(15, hotel.RoomsAvailable);
        }


        [TestMethod]
        public void ParseBestWestern()
        {
            var hotelList = fille.ReadBestWesternHotels();
            var hotel = hotelList[0];
            Assert.AreEqual(50, hotel.RegionValue);
            Assert.AreEqual("Hotell Eggers", hotel.Name);
            Assert.AreEqual(100, hotel.RoomsAvailable);
        }

        [TestMethod]
        public void TestWrongFormatExceptionScandic()
        {
            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt"))
            {
                writetext.WriteLine("");
            }
            Assert.ThrowsException<FormatException>(() => fille.ReadScandicFile());
        }

        [TestMethod]
        public void TestWrongFormatExceptionBestWestern()
        {
            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\BestWestern-{DateTime.Now:yyyy-MM-dd}.json"))
            {
                writetext.WriteLine("");
            }
            Assert.ThrowsException<FormatException>(() => fille.ReadBestWesternHotels());
        }
    }
}
