using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using Test1.Models.AbstractClasses;
using Test1.Models.Entities;
using Test1.Models.Iterfaces;

namespace Test1.Models.Repositories
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
                SELECT Id, FirstName, MiddleName, LastName, 
                       DOB, MobileNumber, Address 
                FROM Employees1 ORDER BY Id;", connection))
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
                SELECT Id, FirstName, MiddleName, LastName, 
                       DOB, MobileNumber, Address 
                FROM Employees1 
                WHERE Id = @Id;", connection))
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

                using (var connection = GetConnection())
                using (var command = new SqlCommand(@"
                INSERT INTO Employees1 
                    (FirstName, MiddleName, LastName, DOB, MobileNumber, Address) 
                VALUES 
                    (@FirstName, @MiddleName, @LastName, @DOB, @MobileNumber, @Address);",
                    connection))
                {
                    MapToParameters(command, entity);

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
                UPDATE Employees1 SET
                    FirstName    = @FirstName,
                    MiddleName   = @MiddleName,
                    LastName     = @LastName,
                    DOB          = @DOB,
                    MobileNumber = @MobileNumber,
                    Address      = @Address
                WHERE Id = @Id;", connection))
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
                    "DELETE FROM Employees1 WHERE Id = @Id", connection))
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

        public void ExecuteQuery3Task()
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "UPDATE Employees1 SET FirstName = 'SQLPerson' WHERE Id = (SELECT TOP 1 Id FROM Employees1 ORDER BY Id);", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Failed to update first name to SQLPerson.", ex);
            }
        }

        public void ExecuteQuery4Task()
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "UPDATE Employees1 SET MiddleName = 'I' WHERE 1 = 1;", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Failed to update middle name to 'I'.", ex);
            }
        }

        public void ExecuteQuery5Task()
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "DELETE FROM Employees1 WHERE Id < 2;", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Failed to delete employee with Id less than 2", ex);
            }
        }

        public void ExecuteQuery6Task()
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "TRUNCATE TABLE Employees1;", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Failed to delete all employees.", ex);
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
                Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString()
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
        }
    }
}