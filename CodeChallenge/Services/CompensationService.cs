using System;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService
    {
        private readonly ILogger<CompensationService> _logger;
        private readonly IEmployeeService _employeeService;

        public CompensationService(ILogger<CompensationService> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public Compensation Create(Compensation compensation)
        {
            throw new NotImplementedException();
        }
        
        public Compensation GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
