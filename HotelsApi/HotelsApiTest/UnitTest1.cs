using System;
using System.IO;
using HotelsApi.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelsApiTest
{
    [TestClass]
    public class UnitTest1
    {
        private FileReader fille = new FileReader();
        [TestInitialize]
        public void Init()
        {
            File.Create($@"wwwroot\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt");
            File.WriteAllText($@"wwwroot\Hotels\Scandic-{DateTime.Now:yyyy-MM-dd}.txt", "50,Scandic Rubinen,15");
        }
        [TestMethod]
        public void TestScandicRubinen()
        {
            Assert.AreEqual(50, fille.ReadScandicFile()[0].RegionValue);
            Assert.AreEqual("Scandic Rubinen", fille.ReadScandicFile()[0].Name);
            Assert.AreEqual(15, fille.ReadScandicFile()[0].RoomsAvailable);
        }
    }
}
