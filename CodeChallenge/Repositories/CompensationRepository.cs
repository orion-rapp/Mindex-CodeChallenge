using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly ILogger<CompensationRepository> _logger;
        private EmployeeContext _employeeContex;

        public CompensationRepository(ILogger<CompensationRepository> logger, EmployeeContext employeeContex)
        {
            _logger = logger;
            _employeeContex = employeeContex;
        }

        public Compensation Add(Compensation compensation)
        {
            compensation.Id = Guid.NewGuid().ToString();
            _employeeContex.Compensations.Add(compensation);
            _employeeContex.SaveChangesAsync().Wait();
            return compensation;
        }

        public Compensation GetById(string id)
        {
            var result = _employeeContex.Compensations.FirstOrDefault(e => e.EmployeeId == id);
            // TODO: Make foreign key relationship in Employee context 
            //       to make the following line of code unecessary.
            result.Employee = _employeeContex.Employees.FirstOrDefault(e => e.EmployeeId == id);
            return result;
        }
    }
}
