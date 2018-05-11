using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsApi.Infrastructure
{
    public class FileReader
    {
        public List<Hotel> ReadScandicFile()
        {
            var hotelList = new List<Hotel>();
            string[] lines = System.IO.File.ReadAllLines($@"Scandic\Scanic-{DateTime.Now:yyyy-MM-dd}.txt");
            foreach (string line in lines)
            {
                var hotelString = line.Split(',');
                hotelList.Add(new Hotel
                {
                });

            }
        }
    }
}
