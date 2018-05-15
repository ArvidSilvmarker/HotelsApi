using System;
using System.IO;
using System.Linq;
using HotelsApi;
using HotelsApi.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelsApiTest
{
    [TestClass]
    public class UnitTest1
    {
        private FileReader fille = new FileReader(new AppConfiguration(){ImportPath = "C:\\Hotels"});

        [TestInitialize]
        public void Init()
        {
            //File.Create($@"C:\Project\AcceleratedLearning\HotelAPI\HotelsApi\HotelsApi\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt");
            //File.WriteAllText($@"C:\Project\AcceleratedLearning\HotelAPI\HotelsApi\HotelsApi\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt", "50,Scandic Rubinen,15");

            using (StreamWriter writetext = new StreamWriter($@"C:\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt"))
            {
                writetext.WriteLine("50,Scandic Rubinen,15");
            }
        }

        [TestMethod]
        public void TestScandicRubinen()
        {
            var hotelList = fille.ReadScandicFile();
            var hotel = hotelList[0];
            Assert.AreEqual(50, hotel.RegionValue);
            Assert.AreEqual("Scandic Rubinen", hotel.Name);
            Assert.AreEqual(15, hotel.RoomsAvailable);
            //Assert.AreEqual(50, fille.ReadScandicFile()[0].RegionValue);
            //Assert.AreEqual("Scandic Rubinen", fille.ReadScandicFile()[0].Name);
            //Assert.AreEqual(15, fille.ReadScandicFile()[0].RoomsAvailable);
        }

    }
}
