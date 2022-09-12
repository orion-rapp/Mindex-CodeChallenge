using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public String Id { get; set; }
        public String EmployeeId { get; set; }  
        public Employee Employee { get; set; } 
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
