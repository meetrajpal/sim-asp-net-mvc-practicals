using System;
using System.Collections.Generic;
using Test2.Models.Entities;
using Test2.Models.Iterfaces;
using Test2.Models.Repositories;

namespace Test2.Models.Services
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
        public decimal TotalSalary()
        {
            if (_employeeRepo is EmployeeRepository employeeRepository)
                return employeeRepository.TotalSalary();
            return 0;
        }
        public IEnumerable<Employee> DOBLT112000()
        {

            if (_employeeRepo is EmployeeRepository employeeRepository)
                return employeeRepository.DOBLT112000();
            return new List<Employee>();
        }

        public IEnumerable<Employee> NullMiddleName()
        {

            if (_employeeRepo is EmployeeRepository employeeRepository)
                return employeeRepository.NullMiddleName();
            return new List<Employee>();
        }
    }
}