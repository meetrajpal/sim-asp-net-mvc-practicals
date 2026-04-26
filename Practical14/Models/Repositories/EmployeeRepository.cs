using Practical14.Models.Data;
using Practical14.Models.Interfaces;
using Practical14.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Practical14.Models.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly Practical14DBEntities _context;

        public EmployeeRepository(Practical14DBEntities context)
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
                    throw new ArgumentNullException(nameof(entity));

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
                    throw new ArgumentNullException(nameof(entity));

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

        public IEnumerable<Employee> Search(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                    return _context.Employees.ToList();

                return _context.Employees
                               .Where(e => e.Name.Contains(keyword))
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to search employees.", ex);
            }
        }

        public PagedResult<Employee> GetPaged(int page, int pageSize)
        {
            try
            {
                var total = _context.Employees.Count();

                var records = _context.Employees
                                      .OrderBy(e => e.Id)
                                      .Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList();

                return new PagedResult<Employee>
                {
                    Records = records,
                    TotalRecords = total,
                    CurrentPage = page,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get paged employees.", ex);
            }
        }
    }
}