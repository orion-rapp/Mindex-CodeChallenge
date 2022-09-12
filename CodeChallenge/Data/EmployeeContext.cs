using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Compensation> Compensations { get; set; }

        /*  TODO: Make relationship by adding Compensation to Emoployee Model (outside the scope of current work)
            example code:
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Compensation>()
                    .HasOne(c => c.Employee)
                    .WithOne(e => e.Compensation) // <-- Requires adding Compensation to Employee Model
                    .HasForeignKey(c => c.EmployeeId);
            }
        */
    }
}
