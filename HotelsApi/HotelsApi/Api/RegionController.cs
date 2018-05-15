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
        private IFileReader _fileReader;

        public RegionController(IRegionRepository regionRepository, AppConfiguration appConfiguration)
        {
            _regionRepository = regionRepository;
            _fileReader = new FileReader(appConfiguration);
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
            try
            {
                var listOfRegions = _regionRepository.ReadAllRegions();
                return Json(listOfRegions);

            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("seed")]
        public IActionResult Seed()
        {
            try
            {
                _regionRepository.ReSeedRegions();
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet("hotels")]
        public IActionResult GetHotels()
        {
            //try
            //{
                var hotelList = _fileReader.ReadAllHotels();
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
            //}
            //catch (Exception exception)
            //{
            //    return BadRequest(exception.Message);
            //}
           
        }

       
    }
}
