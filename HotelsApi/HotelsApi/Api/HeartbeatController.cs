using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelsApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelsApi.Api
{
    [Route("heartbeat")]
    public class HeartbeatController : Controller
    {
        private IRegionRepository _regionRepository;

        public HeartbeatController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public IActionResult SiteIsRuning()
        {
            return Ok("Site is up");
        }

        [HttpGet("database")]
        public IActionResult DatabaseIsRunning()
        {
            if (_regionRepository.IsDatabaseRunning())
                return Ok("Database is running");
            return BadRequest();
        }

        [HttpGet("scandic")]
        public IActionResult ScandicFileToday()
        {
            
        }
    }
}
