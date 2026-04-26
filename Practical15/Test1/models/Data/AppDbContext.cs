using System;
using System.Data.Entity;
using Test1.Models.Data.Configurantions;
using Test1.Models.Entities;

namespace Test1.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base(Environment.GetEnvironmentVariable("Practical15DBString"))
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}