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
            ReportingStructure result = null;
            if (!String.IsNullOrEmpty(id))
            {
                var employee = _employeeService.GetById(id);
                if (employee == null) throw new Exception($"Employee Id {id} not found.");
                var numberOfReports = GetDirectReportsHelper(employee);
                result = new ReportingStructure()
                {
                    Employee = employee,
                    NumberOfReports = numberOfReports
                };
            }
            return result;
        }

        private int GetDirectReportsHelper(Employee employee)
        {
            if (employee.DirectReports != null && employee.DirectReports.Count > 0)
            {
                int result = 0;
                foreach (var dr in employee.DirectReports)
                {
                    result += 1 + GetDirectReportsHelper(dr);
                }
                return result;
            }
            else return 0;
        }
    }
}
