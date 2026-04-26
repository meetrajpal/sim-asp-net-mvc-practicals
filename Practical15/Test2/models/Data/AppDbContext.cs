using System;
using System.Data.Entity;
using Test2.models.Data.Configurantions;
using Test2.models.Entities;
using Test2.Models.Data.Configurantions;
using Test2.Models.Entities;

namespace Test2.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base(Environment.GetEnvironmentVariable("Practical15DBString")) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}