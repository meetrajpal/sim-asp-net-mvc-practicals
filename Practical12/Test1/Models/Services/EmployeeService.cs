using System;
using System.Collections.Generic;
using Test1.Models.Entities;
using Test1.Models.Iterfaces;
using Test1.Models.Repositories;

namespace Test1.Models.Services
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeService(IRepository<Employee> repository)
        {
            _employeeRepo = repository;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeRepo.GetAll();
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

            if (string.IsNullOrWhiteSpace(employee.FirstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(employee.LastName))
                throw new ArgumentException("Last name is required.");

            if (employee.DOB == default)
                throw new ArgumentException("Date of birth is required.");

            if (string.IsNullOrWhiteSpace(employee.MobileNumber))
                throw new ArgumentException("Mobile number is required.");

            if (employee.MobileNumber.Length != 10)
                throw new ArgumentException("Mobile number must be 10 digits.");

            _employeeRepo.Add(employee);
        }

        public void Update(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (string.IsNullOrWhiteSpace(employee.FirstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(employee.LastName))
                throw new ArgumentException("Last name is required.");

            if (employee.DOB == default)
                throw new ArgumentException("Date of birth is required.");

            if (string.IsNullOrWhiteSpace(employee.MobileNumber))
                throw new ArgumentException("Mobile number is required.");

            if (employee.MobileNumber.Length != 10)
                throw new ArgumentException("Mobile number must be 10 digits.");

            _employeeRepo.Update(employee);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.");

            _employeeRepo.Delete(id);
        }

        public void ExecuteQuery3Task()
        {
            if (_employeeRepo is EmployeeRepository employeeRepository)
                employeeRepository.ExecuteQuery3Task();
        }
        public void ExecuteQuery4Task()
        {
            if (_employeeRepo is EmployeeRepository employeeRepository)
                employeeRepository.ExecuteQuery4Task();
        }
        public void ExecuteQuery5Task()
        {
            if (_employeeRepo is EmployeeRepository employeeRepository)
                employeeRepository.ExecuteQuery5Task();
        }
        public void ExecuteQuery6Task()
        {
            if (_employeeRepo is EmployeeRepository employeeRepository)
                employeeRepository.ExecuteQuery6Task();
        }
    }
}