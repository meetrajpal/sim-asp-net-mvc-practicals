using Microsoft.Data.SqlClient;
using System;

namespace Test3.Models.AbstractClasses
{
    public abstract class BaseRepository
    {
        protected readonly string _connectionString;

        protected BaseRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("Practical12DBString");
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}