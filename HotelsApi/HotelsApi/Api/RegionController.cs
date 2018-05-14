using HotelsApi.Domain;
using HotelsApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using HotelsApi.Infrastructure;

namespace HotelsApi.Controllers
{
    [Route("regions")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository _regionRepository;

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

            try
            {
                _regionRepository.CreateRegion(region);
                return Ok(region.Id);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete, Route("{id:int}")]
        public IActionResult DeleteRegion(int id)
        {
            try
            {
                var region = _regionRepository.ReadRegion(id);
                if (region == null)
                    return NoContent();

                _regionRepository.DeleteRegion(id);
                return Ok("Deleted");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllReagions()
        {
            var listOfRegions = _regionRepository.ReadAllRegions();
            return Json(listOfRegions);
        }

        [HttpPost("seed")]
        public IActionResult Seed()
        {
            _regionRepository.ReSeedRegions();
            return Ok();
        }

        [HttpGet("hotels")]
        public IActionResult GetHotels()
        {
            try
            {
                IFileReader fileReader = new FileReader();

                var hotelList = fileReader.ReadScandicFile();
                var listOfRegions = _regionRepository.ReadAllRegions();
                foreach (var region in listOfRegions)
                {
                    foreach (var hotel in hotelList)
                    {
                        if (region.Value == hotel.RegionValue)
                        {
                            region.Hotels.Add(hotel);
                        }
                    }
                }
                return Json(listOfRegions);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

       
    }
}
