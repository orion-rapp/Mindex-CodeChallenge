using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository
    {
        private readonly ILogger<CompensationRepository> _logger;
        private EmployeeContext _employeeContext;

        public CompensationRepository(ILogger<CompensationRepository> logger, EmployeeContext employeeContext)
        {
            _logger = logger;
            _employeeContext = employeeContext;
        }

        public Compensation Add(Compensation compensation)
        {
            throw new NotImplementedException();
        }

        public Compensation GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
