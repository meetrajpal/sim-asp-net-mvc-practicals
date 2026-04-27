CREATE DATABASE Practical12DB;
GO

USE Practical12DB;
GO

CREATE SCHEMA Test1
GO

CREATE TABLE Test1.Employees
(
    Id           INT PRIMARY KEY IDENTITY(1,1),
    FirstName    NVARCHAR(50)  NOT NULL,
    MiddleName   NVARCHAR(50)  NULL,
    LastName     NVARCHAR(50)  NOT NULL,
    DOB          DATE          NOT NULL,
    MobileNumber NVARCHAR(10)  NOT NULL,
    Address      NVARCHAR(100) NULL
);
GO

INSERT INTO Test1.Employees (FirstName, MiddleName, LastName, DOB, MobileNumber, Address) VALUES
('John',  'William', 'Doe',     '2000-05-15', '9876543210', '123 Main St, New York'),
('Jane',  NULL,      'Smith',   '1999-08-22', '8765432109', NULL),
('Alice', 'Mary',    'Johnson', '2001-03-10', '7654321098', '456 Park Ave, London'),
('Bob',   NULL,      'Brown',   '2000-11-30', '6543210987', '789 Oak Rd, Texas'),
('Charlie','James',  'Wilson',  '1998-07-19', '5432109876', NULL);
GO
