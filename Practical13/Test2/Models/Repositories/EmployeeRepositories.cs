using System;
using System.Collections.Generic;
using System.Linq;
using Test2.Models.Data;
using Test2.Models.Entities;
using Test2.Models.Interfaces;
using Test2.Models.ViewModels;

namespace Test2.Models.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            try
            {
                return _context.Employees
                               .Include("Designation")
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve employees.", ex);
            }
        }

        public Employee GetById(int id)
        {
            try
            {
                return _context.Employees
                               .Include("Designation")
                               .FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve employee with id {id}.", ex);
            }
        }

        public void Add(Employee entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Employee cannot be null.");

                _context.Employees.Add(entity);
                _context.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add employee.", ex);
            }
        }

        public void Update(Employee entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Employee cannot be null.");

                var existing = _context.Employees.Find(entity.Id);

                if (existing == null)
                    throw new KeyNotFoundException($"Employee with id {entity.Id} not found.");

                existing.FirstName = entity.FirstName;
                existing.MiddleName = entity.MiddleName;
                existing.LastName = entity.LastName;
                existing.DOB = entity.DOB;
                existing.MobileNumber = entity.MobileNumber;
                existing.Address = entity.Address;
                existing.Salary = entity.Salary;
                existing.DesignationId = entity.DesignationId;

                _context.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update employee.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = _context.Employees.Find(id);

                if (entity == null)
                    throw new KeyNotFoundException($"Employee with id {id} not found.");

                _context.Employees.Remove(entity);
                _context.SaveChanges();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete employee with id {id}.", ex);
            }
        }

        public IEnumerable<DesignationEmployeeCount> GetCountByDesignation()
        {
            try
            {
                return _context.Employees
                               .Where(e => e.DesignationId != null)
                               .GroupBy(e => e.Designation.DesignationName)
                               .Select(g => new DesignationEmployeeCount
                               {
                                   DesignationName = g.Key,
                                   EmployeeCount = g.Count()
                               })
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get employee count by designation.", ex);
            }
        }
    }
}