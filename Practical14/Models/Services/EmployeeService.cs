using Practical14.Models.Data;
using Practical14.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Practical14.Models.Services
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeService(IRepository<Employee> repository)
        {
            _employeeRepo = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeRepo.GetAll();
        }

        public IEnumerable<Employee> Search(string keyword)
        {
            return _employeeRepo.Search(keyword);
        }

        public Employee GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.");

            var employee = _employeeRepo.GetById(id);

            if (employee == null)
                throw new KeyNotFoundException($"Employee with id {id} not found.");

            return employee;
        }

        public void Create(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (string.IsNullOrWhiteSpace(employee.Name))
                throw new ArgumentException("Name is required.");

            if (employee.DOB == default)
                throw new ArgumentException("Date of birth is required.");

            if (employee.Age.HasValue && employee.Age <= 0)
                throw new ArgumentException("Age must be greater than zero.");

            _employeeRepo.Add(employee);
        }


        public void Update(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (string.IsNullOrWhiteSpace(employee.Name))
                throw new ArgumentException("Name is required.");

            if (employee.DOB == default)
                throw new ArgumentException("Date of birth is required.");

            if (employee.Age.HasValue && employee.Age <= 0)
                throw new ArgumentException("Age must be greater than zero.");

            _employeeRepo.Update(employee);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.");

            _employeeRepo.Delete(id);
        }
    }
}