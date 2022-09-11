using CodeChallenge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger<ReportingStructureController> _logger;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, 
                                            IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(string id)
        {
            _logger.LogDebug($"Received report structure get request for '{id}'");

            var reportStructure = _reportingStructureService.GetReportingStructureById(id);

            if (reportStructure == null)
                return NotFound();

            return Ok(reportStructure);
        }
        
    }
}
