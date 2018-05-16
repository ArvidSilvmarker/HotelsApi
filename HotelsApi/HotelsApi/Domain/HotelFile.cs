using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsApi.Domain
{
    public class HotelFile
    {
        public DateTime Date { get; set; }
        public string Path { get; set; }
        public bool Exists { get; set; }
    }
}
