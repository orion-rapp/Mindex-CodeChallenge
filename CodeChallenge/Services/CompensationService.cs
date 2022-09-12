using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ILogger<CompensationService> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly ICompensationRepository _compensationRepository;

        public CompensationService( ILogger<CompensationService> logger, IEmployeeService employeeService, 
                                    ICompensationRepository compensationRepository)
        {
            _logger = logger;
            _employeeService = employeeService;
            _compensationRepository = compensationRepository;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation == null) throw new Exception($"Cannot Create Compensation: {compensation}");

            return _compensationRepository.Add(compensation);
        }

        public Compensation GetById(string id)
        {
            return _compensationRepository.GetById(id);
        }
    }
}
