using System;
using System.Data.Entity;
using Test2.Models.Data.Configuration;
using Test2.Models.Entities;

namespace Test2.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base(Environment.GetEnvironmentVariable("Practical13DBString"))
        {
        }

        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DesignationConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}