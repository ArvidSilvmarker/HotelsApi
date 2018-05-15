using System;
using System.Collections.Generic;
using System.Text;
using HotelsApi.Common;
using HotelsApi.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HotelsApiTest
{
    [TestClass]
    public class TestValidation
    {
        [TestInitialize]
        public void Init()
        {
        }
        [TestMethod]
        public void TestGoodHotelValidation()
        {
            var goodHotel = new Hotel { Name = "Blåbär", RegionValue = 20, RoomsAvailable = 500 };

            Assert.AreEqual(true, HotelValidation.ValidateHotel(goodHotel));
        }

        [TestMethod]
        public void TestBadHotelWithoutNameValidation()
        {
            var badHotel = new Hotel { RegionValue = -1, RoomsAvailable = -3 };

            Assert.AreEqual(false, HotelValidation.ValidateHotel(badHotel));
        }

        [TestMethod]
        public void TestBadHotelRegionValue()
        {
            var hotelWithBadRegion = new Hotel{Name = "hotel", RegionValue = -5, RoomsAvailable = 3};
            Assert.AreEqual(false, HotelValidation.ValidateHotel(hotelWithBadRegion));
        }

        [TestMethod]
        public void TestBadHotelRoomsAvailable()
        {
            var badhotel = new Hotel{Name = "hotel", RegionValue = 20, RoomsAvailable = -5};
            Assert.AreEqual(false, HotelValidation.ValidateHotel(badhotel));
        }
    }
}
