USE Practical12DB;
GO

CREATE SCHEMA Test2
GO

CREATE TABLE Test2.Employees
(
    Id           INT PRIMARY KEY IDENTITY(1,1),
    FirstName    NVARCHAR(50)   NOT NULL,
    MiddleName   NVARCHAR(50)   NULL,
    LastName     NVARCHAR(50)   NOT NULL,
    DOB          DATE           NOT NULL,
    MobileNumber NVARCHAR(10)   NOT NULL,
    Address      NVARCHAR(100)  NULL,
    Salary       DECIMAL(13, 2) NOT NULL
);
GO

INSERT INTO Test2.Employees (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary) VALUES
('John',  'William', 'Doe',     '2000-05-15', '9876543210', '123 Main St, New York', 12345.25),
('Jane',  NULL,      'Smith',   '1999-08-22', '8765432109', NULL, 98745.00),
('Alice', 'Mary',    'Johnson', '2001-03-10', '7654321098', '456 Park Ave, London', 78542.00),
('Bob',   NULL,      'Brown',   '2000-11-30', '6543210987', '789 Oak Rd, Texas', 12317.25),
('Charlie','James',  'Wilson',  '1998-07-19', '5432109876', NULL, 105420.00);
GO
