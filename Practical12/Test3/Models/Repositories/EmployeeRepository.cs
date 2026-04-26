using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Test3.Models.AbstractClasses;
using Test3.Models.Entities;
using Test3.Models.Iterfaces;
using Test3.Models.ViewModels;

namespace Test3.Models.Repositories
{
    public class EmployeeRepository : BaseRepository, IRepository<Employee>
    {
        public IEnumerable<Employee> GetAll()
        {
            var employees = new List<Employee>();

            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(@"
                SELECT e.Id, e.FirstName, e.MiddleName, e.LastName,
                       e.DOB, e.MobileNumber, e.Address, e.Salary,
                       e.DesignationId, d.DesignationName
                FROM Test3.Employees e
                LEFT JOIN Test3.Designation d ON e.DesignationId = d.Id", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            employees.Add(MapToEmployee(reader));
                    }
                }

                return employees;
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to retrieve employees.", ex);
            }
        }

        public Employee GetById(int id)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(@"
                SELECT e.Id, e.FirstName, e.MiddleName, e.LastName,
                       e.DOB, e.MobileNumber, e.Address, e.Salary,
                       e.DesignationId, d.DesignationName
                FROM Test3.Employees e
                LEFT JOIN Test3.Designation d ON e.DesignationId = d.Id
                WHERE e.Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapToEmployee(reader);

                        return null;
                    }
                }
            }
            catch (SqlException ex)
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

                var dataTable = new DataTable();
                dataTable.Columns.Add("FirstName", typeof(string));
                dataTable.Columns.Add("MiddleName", typeof(string));
                dataTable.Columns.Add("LastName", typeof(string));
                dataTable.Columns.Add("DOB", typeof(DateTime));
                dataTable.Columns.Add("MobileNumber", typeof(string));
                dataTable.Columns.Add("Address", typeof(string));
                dataTable.Columns.Add("Salary", typeof(decimal));
                dataTable.Columns.Add("DesignationId", typeof(int));

                dataTable.Rows.Add(
                    entity.FirstName,
                    (object)entity.MiddleName ?? DBNull.Value,
                    entity.LastName,
                    entity.DOB,
                    entity.MobileNumber,
                    (object)entity.Address ?? DBNull.Value,
                    entity.Salary,
                    (object)entity.DesignationId ?? DBNull.Value
                );

                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("Test3.sp_InsertEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var param = command.Parameters.AddWithValue("@EmployeeData", dataTable);
                    param.SqlDbType = SqlDbType.Structured;
                    param.TypeName = "Test3.EmployeeType";

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (SqlException ex)
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

                using (var connection = GetConnection())
                using (var command = new SqlCommand(@"
                UPDATE Test3.Employees SET
                    FirstName     = @FirstName,
                    MiddleName    = @MiddleName,
                    LastName      = @LastName,
                    DOB           = @DOB,
                    MobileNumber  = @MobileNumber,
                    Address       = @Address,
                    Salary        = @Salary,
                    DesignationId = @DesignationId
                WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    MapToParameters(command, entity);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                        throw new KeyNotFoundException($"Employee with id {entity.Id} not found.");
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to update employee.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "DELETE FROM Test3.Employees WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                        throw new KeyNotFoundException($"Employee with id {id} not found.");
                }
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Failed to delete employee with id {id}.", ex);
            }
        }

        public IEnumerable<EmployeeSummary> GetEmployeeSummary()
        {
            var result = new List<EmployeeSummary>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(@"
                SELECT
                    e.FirstName,
                    e.MiddleName,
                    e.LastName,
                    d.DesignationName
                FROM Test3.Employees e
                LEFT JOIN Test3.Designation d ON e.DesignationId = d.Id", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new EmployeeSummary
                            {
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader["MiddleName"] == DBNull.Value ? null : reader["MiddleName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DesignationName = reader["DesignationName"] == DBNull.Value ? null : reader["DesignationName"].ToString()
                            });
                        }
                    }
                }

                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to get employee summary.", ex);
            }
        }

        public IEnumerable<Employee> GetAllOrderedByDOB()
        {
            var employees = new List<Employee>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("Test3.sp_GetAllEmployees", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            employees.Add(MapToEmployee(reader));
                    }
                }

                return employees;
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to get employees ordered by DOB.", ex);
            }
        }

        public IEnumerable<Employee> GetByDesignationId(int designationId)
        {
            var employees = new List<Employee>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("Test3.sp_GetEmployeesByDesignation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DesignationId", designationId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            employees.Add(MapToEmployee(reader));
                    }
                }

                return employees;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Failed to get employees for designation {designationId}.", ex);
            }
        }

        public Employee GetMaxSalaryEmployee()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(@"
                SELECT TOP 1
                    e.Id, e.FirstName, e.MiddleName, e.LastName,
                    e.DOB, e.MobileNumber, e.Address, e.Salary,
                    e.DesignationId, d.DesignationName
                FROM Test3.Employees e
                LEFT JOIN Test3.Designation d ON e.DesignationId = d.Id
                ORDER BY e.Salary DESC", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapToEmployee(reader);

                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to get employee with maximum salary.", ex);
            }
        }

        private Employee MapToEmployee(SqlDataReader reader)
        {
            return new Employee
            {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"].ToString(),
                MiddleName = reader["MiddleName"] == DBNull.Value ? null : reader["MiddleName"].ToString(),
                LastName = reader["LastName"].ToString(),
                DOB = (DateTime)reader["DOB"],
                MobileNumber = reader["MobileNumber"].ToString(),
                Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                Salary = (decimal)reader["Salary"],
                DesignationId = reader["DesignationId"] == DBNull.Value ? (int?)null : (int)reader["DesignationId"],
                DesignationName = reader["DesignationName"] == DBNull.Value ? null : reader["DesignationName"].ToString()
            };
        }

        private void MapToParameters(SqlCommand command, Employee entity)
        {
            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@MiddleName", (object)entity.MiddleName ?? DBNull.Value);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@DOB", entity.DOB);
            command.Parameters.AddWithValue("@MobileNumber", entity.MobileNumber);
            command.Parameters.AddWithValue("@Address", (object)entity.Address ?? DBNull.Value);
            command.Parameters.AddWithValue("@Salary", entity.Salary);
            command.Parameters.AddWithValue("@DesignationId", (object)entity.DesignationId ?? DBNull.Value);
        }
    }
}