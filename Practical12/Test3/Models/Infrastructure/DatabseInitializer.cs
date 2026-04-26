using Microsoft.Data.SqlClient;
using System;

namespace Test3.Models.Infrastructure
{
    public class DatabaseInitializer
    {
        private static readonly string _connectionString;

        static DatabaseInitializer()
        {
            _connectionString = Environment.GetEnvironmentVariable("Practical12DBString");
        }

        public static void Initialize()
        {
            CreateSchema();
            CreateTables();
            CreateTableTypes();
            CreateView();
            CreateIndex();
            CreateStoredProcedures();
            SeedData();
        }

        private static void ExecuteQuery(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static void CreateSchema()
        {
            ExecuteQuery(@"
            IF NOT EXISTS (
                SELECT * FROM sys.schemas
                WHERE name = 'Test3'
            )
            BEGIN
                EXEC('CREATE SCHEMA Test3')
            END");
        }

        private static void CreateTables()
        {
            ExecuteQuery(@"
                IF NOT EXISTS (
                    SELECT * FROM sysobjects
                    WHERE name = 'Designation' AND xtype = 'U'
                )
                BEGIN
                    CREATE TABLE Test3.Designation
                    (
                        Id          INT PRIMARY KEY IDENTITY(1,1),
                        DesignationName NVARCHAR(50) NOT NULL
                    )
                END");

            ExecuteQuery(@"
                IF NOT EXISTS (
                    SELECT * FROM sysobjects
                    WHERE name = 'Employees' AND xtype = 'U'
                )
                BEGIN
                    CREATE TABLE Test3.Employees
                    (
                        Id            INT PRIMARY KEY IDENTITY(1,1),
                        FirstName     NVARCHAR(50)  NOT NULL,
                        MiddleName    NVARCHAR(50)  NULL,
                        LastName      NVARCHAR(50)  NOT NULL,
                        DOB           DATE          NOT NULL,
                        MobileNumber  NVARCHAR(10)  NOT NULL,
                        Address       NVARCHAR(100) NULL,
                        Salary        DECIMAL(18,2) NOT NULL,
                        DesignationId INT           NULL,

                        CONSTRAINT FK_Employees_Designation
                            FOREIGN KEY (DesignationId) REFERENCES Test3.Designation(Id)
                    )
                END");
        }

        private static void CreateTableTypes()
        {

            ExecuteQuery(@"
                IF NOT EXISTS (
                    SELECT * FROM sys.types
                    WHERE name = 'DesignationType'
                    AND schema_id = SCHEMA_ID('Test3')
                )
                BEGIN
                    CREATE TYPE Test3.DesignationType AS TABLE
                    (
                        DesignationName NVARCHAR(50) NOT NULL
                    )
                END");


            ExecuteQuery(@"
                IF NOT EXISTS (
                    SELECT * FROM sys.types
                    WHERE name = 'EmployeeType'
                    AND schema_id = SCHEMA_ID('Test3')
                )
                BEGIN
                    CREATE TYPE Test3.EmployeeType AS TABLE
                    (
                        FirstName     NVARCHAR(50)  NOT NULL,
                        MiddleName    NVARCHAR(50)  NULL,
                        LastName      NVARCHAR(50)  NOT NULL,
                        DOB           DATE          NOT NULL,
                        MobileNumber  NVARCHAR(10)  NOT NULL,
                        Address       NVARCHAR(100) NULL,
                        Salary        DECIMAL(18,2) NOT NULL,
                        DesignationId INT           NULL
                    )
                END");
        }

        private static void CreateView()
        {
            ExecuteQuery(@"
            CREATE OR ALTER VIEW Test3.vw_EmployeeDetails AS
            SELECT
                e.Id,
                e.FirstName,
                e.MiddleName,
                e.LastName,
                d.DesignationName,
                e.DOB,
                e.MobileNumber,
                e.Address,
                e.Salary
            FROM Test3.Employees e
            LEFT JOIN Test3.Designation d ON e.DesignationId = d.Id");
        }

        private static void CreateIndex()
        {
            ExecuteQuery(@"
                IF NOT EXISTS (
                    SELECT * FROM sys.indexes
                    WHERE name = 'IX_Employees_DesignationId'
                    AND object_id = OBJECT_ID('Test3.Employees')
                )
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_Employees_DesignationId
                    ON Test3.Employees(DesignationId)
                END");
        }

        private static void CreateStoredProcedures()
        {

            ExecuteQuery(@"
            CREATE OR ALTER PROCEDURE Test3.sp_InsertDesignation
                @DesignationData Test3.DesignationType READONLY
            AS
            BEGIN
                SET NOCOUNT ON;

                INSERT INTO Test3.Designation (DesignationName)
                SELECT DesignationName FROM @DesignationData
            END");



            ExecuteQuery(@"
                CREATE OR ALTER PROCEDURE Test3.sp_InsertEmployee
                    @EmployeeData Test3.EmployeeType READONLY
                AS
                BEGIN
                    SET NOCOUNT ON;

                    INSERT INTO Test3.Employees
                        (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId)
                    SELECT
                        FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId
                    FROM @EmployeeData
                END");


            ExecuteQuery(@"
                CREATE OR ALTER PROCEDURE Test3.sp_GetAllEmployees
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT
                        e.Id,
                        e.FirstName,
                        e.MiddleName,
                        e.LastName,
                        d.DesignationName,
                        e.DOB,
                        e.MobileNumber,
                        e.Address,
                        e.Salary,
                        e.DesignationId
                    FROM Test3.Employees e
                    LEFT JOIN Test3.Designation d ON e.DesignationId = d.Id
                    ORDER BY e.DOB
                END");


            ExecuteQuery(@"
                CREATE OR ALTER PROCEDURE Test3.sp_GetEmployeesByDesignation
                    @DesignationId INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT
                        e.Id,
                        e.FirstName,
                        e.MiddleName,
                        e.LastName,
                        e.DOB,
                        e.MobileNumber,
                        e.Address,
                        e.Salary,
                        e.DesignationId,
                        d.DesignationName
                    FROM Test3.Employees e 
                    JOIN Test3.Designation d ON e.DesignationId = d.Id
                    WHERE e.DesignationId = @DesignationId
                    ORDER BY e.FirstName
                END");
        }

        private static void SeedData()
        {
            ExecuteQuery(@"
            IF NOT EXISTS (SELECT TOP 1 1 FROM Test3.Designation)
            BEGIN
                INSERT INTO Test3.Designation (DesignationName) VALUES
                ('Software Engineer'),
                ('Senior Software Engineer'),
                ('Team Lead'),
                ('Project Manager'),
                ('HR Manager')
            END");

            ExecuteQuery(@"
                IF NOT EXISTS (SELECT TOP 1 1 FROM Test3.Employees)
                BEGIN
                    INSERT INTO Test3.Employees
                        (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId)
                    VALUES
                    ('John',    'William', 'Doe',     '2000-05-15', '9876543210', '123 Main St, New York', 55000.00, 1),
                    ('Jane',    NULL,      'Smith',   '1999-08-22', '8765432109', NULL,                    75000.00, 3),
                    ('Alice',   'Mary',    'Johnson', '2001-03-10', '7654321098', '456 Park Ave, London',  45000.00, 1),
                    ('Bob',     NULL,      'Brown',   '2000-11-30', '6543210987', '789 Oak Rd, Texas',     95000.00, 4),
                    ('Charlie', 'James',   'Wilson',  '1998-07-19', '5432109876', NULL,                    65000.00, 2)
                END");
        }
    }
}