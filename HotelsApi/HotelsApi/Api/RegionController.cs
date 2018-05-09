using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelsApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelsApi.Controllers
{
    [Route("regions")]
    public class RegionController : Controller
    {
        private IRegionRepository _regionRepository;

        public RegionController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }




        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
