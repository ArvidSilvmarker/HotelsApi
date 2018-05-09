using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsApi.Domain
{
    public class Seed
    {
        public List<Region> SeedRegions()
        {
            //Göteborg Centrum    50
            //Göteborg Hisingen   60
            //Helsingborg         70

            return new List<Region>
            {
                new Region
                {
                    Value = 50,
                    Name = "Göteborg Centrum"
                },
                new Region
                {
                    Value = 60,
                    Name = "Göteborg Hisingen"
                },
                new Region
                {
                    Value = 70,
                    Name = "Helsingborg"
                }

            };
        }
    }
}
