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
        private IFileReader _fileReader;

        public HeartbeatController(IRegionRepository regionRepository, IFileReader fileReader)
        {
            _regionRepository = regionRepository;
            _fileReader = fileReader;
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
            if (_fileReader.LatestScandicFile(DateTime.Now).Date == DateTime.Now.Date)
                return Ok("Ffffound");

            return NoContent();
        }

        [HttpGet("bestwestern")]
        public IActionResult BestWesternFileToday()
        {
            if (_fileReader.LatestBestWesternFile(DateTime.Now).Date == DateTime.Now.Date)
                return Ok("Ffffound");
            return NoContent();
        }

        [HttpGet("scandic/soft")]
        public IActionResult ScandicFileTodayOrYesterday()
        {
            var scandicFile = _fileReader.LatestScandicFile(DateTime.Now);
            if (scandicFile.Date == DateTime.Now.Date || (scandicFile.Date == DateTime.Now.AddDays(-1).Date && DateTime.Now.Hour < 10 ))
                return Ok("Ffffound");

            return NoContent();
        }

        [HttpGet("bestwestern/soft")]
        public IActionResult BestWesternFileTodayOrYesterday()
        {
            var bestWesternFile = _fileReader.LatestBestWesternFile(DateTime.Now);
            if (bestWesternFile.Date == DateTime.Now.Date || (bestWesternFile.Date == DateTime.Now.AddDays(-1).Date && DateTime.Now.Hour < 10))
                return Ok("Ffffound");

            return NoContent();
        }
    }
}
