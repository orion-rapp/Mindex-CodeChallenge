using CodeChallenge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    /// <summary>
    /// In the original ReadMe.md, it didn't specify if the new endpoint for ReportingStructure
    /// should exist in a new controller or if it should exist on it's own controller.
    /// For flexibility/thoroughness I implemented the endpoint in both places.
    /// </summary>
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
