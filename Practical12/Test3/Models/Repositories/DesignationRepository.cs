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
    public class DesignationRepository : BaseRepository, IRepository<Designation>
    {
        public IEnumerable<Designation> GetAll()
        {
            var designations = new List<Designation>();

            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "SELECT Id, DesignationName FROM Test3.Designation", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            designations.Add(MapToDesignation(reader));
                    }
                }

                return designations;
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to retrieve designations.", ex);
            }
        }

        public Designation GetById(int id)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "SELECT Id, DesignationName FROM Test3.Designation WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapToDesignation(reader);

                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Failed to retrieve designation with id {id}.", ex);
            }
        }

        public void Add(Designation entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Designation cannot be null.");

                var dataTable = new DataTable();
                dataTable.Columns.Add("DesignationName", typeof(string));
                dataTable.Rows.Add(entity.DesignationName);

                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("Test3.sp_InsertDesignation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var param = command.Parameters.AddWithValue("@DesignationData", dataTable);
                    param.SqlDbType = SqlDbType.Structured;
                    param.TypeName = "Test3.DesignationType";

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
                throw new Exception("Failed to add designation.", ex);
            }
        }

        public void Update(Designation entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Designation cannot be null.");

                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "UPDATE Test3.Designation SET DesignationName = @Designation WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@Designation", entity.DesignationName);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                        throw new KeyNotFoundException($"Designation with id {entity.Id} not found.");
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
                throw new Exception("Failed to update designation.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "DELETE FROM Test3.Designation WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                        throw new KeyNotFoundException($"Designation with id {id} not found.");
                }
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Failed to delete designation with id {id}.", ex);
            }
        }

        public IEnumerable<DesignationCount> GetCountByDesignation()
        {
            var result = new List<DesignationCount>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(@"
                    SELECT d.DesignationName, COUNT(e.Id) AS EmployeeCount
                    FROM Test3.Designation d
                    LEFT JOIN Test3.Employees e ON d.Id = e.DesignationId
                    GROUP BY d.DesignationName", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new DesignationCount
                            {
                                DesignationName = reader["DesignationName"].ToString(),
                                EmployeeCount = (int)reader["EmployeeCount"]
                            });
                        }
                    }
                }

                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to get designation counts.", ex);
            }
        }

        public IEnumerable<DesignationCount> GetDesignationsWithMultipleEmployees()
        {
            var result = new List<DesignationCount>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(@"
                    SELECT d.DesignationName, COUNT(e.Id) AS EmployeeCount
                    FROM Test3.Designation d
                    INNER JOIN Test3.Employees e ON d.Id = e.DesignationId
                    GROUP BY d.DesignationName
                    HAVING COUNT(e.Id) > 1", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new DesignationCount
                            {
                                DesignationName = reader["DesignationName"].ToString(),
                                EmployeeCount = (int)reader["EmployeeCount"]
                            });
                        }
                    }
                }

                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to get designations with multiple employees.", ex);
            }
        }

        private Designation MapToDesignation(SqlDataReader reader)
        {
            return new Designation
            {
                Id = (int)reader["Id"],
                DesignationName = reader["DesignationName"].ToString()
            };
        }


    }
}