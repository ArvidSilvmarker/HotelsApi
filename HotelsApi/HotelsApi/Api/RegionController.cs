using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelsApi.Domain;
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

        [HttpPost]
        public IActionResult AddRegion(Region region)
        {
            if (region == null)
            {
                return BadRequest("Region is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _regionRepository.CreateRegion(region);

            return Ok(region.Id);
        }

        [HttpPost, Route("delete")]
        public IActionResult DeleteRegion(Region region)
        {
            _regionRepository.DeleteRegion(region);
            return Ok("Deleted");
        }

        [HttpGet]
        public IActionResult GetAllReagions()
        {
            var listOfRegions = _regionRepository.ReadAllRegions();
            return Json(listOfRegions);
        }

        [HttpPost("Seed")]
        public IActionResult Seed()
        {
            _regionRepository.ReSeedRegions();
            return Ok();
        }

       
    }
}
