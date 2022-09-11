using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly ILogger<ReportingStructureService> _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public ReportingStructure GetReportingStructureById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
