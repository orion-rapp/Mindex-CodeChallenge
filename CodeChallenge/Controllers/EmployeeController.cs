using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IReportingStructureService _reportingStructureService;
        private readonly ICompensationService _compensationService;

        public EmployeeController(  ILogger<EmployeeController> logger, 
                                    IEmployeeService employeeService,   
                                    IReportingStructureService reportingStructureService,
                                    ICompensationService compensationService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _reportingStructureService = reportingStructureService;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }
        
        [HttpPost("{id}/compensation")]
        public IActionResult CreateEmployeeComensation(String id, [FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Recieved compensation create request for '{id}'");

            // Since Employee is a required property of Compensation, check for Employee entity by Id first
            var employee = _employeeService.GetById(id);
            if (employee == null)
                return NotFound();

            compensation.EmployeeId = id;
            var result = _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationById", new { id = id }, compensation);
        }


        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpGet("{id}/ReportingStructure")]
        public IActionResult GetReportingStructureById(string id)
        {
            _logger.LogDebug($"Received report structure get request for '{id}'");

            var reportStructure = _reportingStructureService.GetReportingStructureById(id);

            if (reportStructure == null)
                return NotFound();

            return Ok(reportStructure);
        }

        [HttpGet("{id}/Compensation", Name = "getCompensationById")]
        public IActionResult GetEmployeeCompensationById(string id)
        {
            _logger.LogDebug($"Recieved compensation get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }


        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();
            
            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }
    }
}
