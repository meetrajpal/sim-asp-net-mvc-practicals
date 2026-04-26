using System;
using System.Collections.Generic;
using Test3.Models.Entities;
using Test3.Models.Iterfaces;
using Test3.Models.Repositories;
using Test3.Models.ViewModels;

namespace Test3.Models.Services
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

            if (employee.Salary <= 0)
                throw new ArgumentException("Salary must be greater than zero.");

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

            if (employee.Salary <= 0)
                throw new ArgumentException("Salary must be greater than zero.");

            _employeeRepo.Update(employee);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.");

            _employeeRepo.Delete(id);
        }

        public IEnumerable<EmployeeSummary> GetEmployeeSummary()
        {
            if (_employeeRepo is EmployeeRepository employeeRepository)
                return employeeRepository.GetEmployeeSummary();

            return new List<EmployeeSummary>();
        }

        public IEnumerable<Employee> GetAllOrderedByDOB()
        {
            if (_employeeRepo is EmployeeRepository employeeRepository)
                return employeeRepository.GetAllOrderedByDOB();
            return new List<Employee>();
        }

        public IEnumerable<Employee> GetByDesignationId(int designationId)
        {
            if (designationId <= 0)
                throw new ArgumentException("Designation id must be greater than zero.");

            if (_employeeRepo is EmployeeRepository employeeRepository)
                return employeeRepository.GetByDesignationId(designationId);

            return new List<Employee>();
        }

        public Employee GetMaxSalaryEmployee()
        {
            if (_employeeRepo is EmployeeRepository employeeRepository)
            {
                var employee = employeeRepository.GetMaxSalaryEmployee();

                if (employee == null)
                    throw new KeyNotFoundException("No employees found.");

                return employee;
            }
            return null;
        }
    }
}