using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;
        private readonly List<Employee> _repoAsList;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;

            // In the given solution, prior to any of my changes,
            // the dbContext would not save the FixUpReferences version of the data.
            // For the purposes of this evaluation, I coded the following work around such that I could get to the
            // direct report data my new features depend on.
            _repoAsList = new EmployeeDataSeeder(
                              new EmployeeContext(
                              new DbContextOptionsBuilder<EmployeeContext>().UseInMemoryDatabase("EmployeeDB").Options
                                                  )).LoadEmployees();
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            var result = _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
            if (result.DirectReports == null) // null if they have direct reports but dbContext isn't saving propperly after FixUpReferences() runs
                                              // or if they really do not have direct reports
            {
                result = _repoAsList.SingleOrDefault(e => e.EmployeeId == id);
            }
            return result;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
