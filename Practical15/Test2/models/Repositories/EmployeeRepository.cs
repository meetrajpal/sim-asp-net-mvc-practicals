using System;
using System.Collections.Generic;
using System.Linq;
using Test2.Models.Data;
using Test2.Models.Entities;
using Test2.Models.Interfaces;

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
                return _context.Employees.ToList();
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
                return _context.Employees.Find(id);
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

                existing.Name = entity.Name;
                existing.DOB = entity.DOB;
                existing.Age = entity.Age;

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
    }
}