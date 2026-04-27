USE Practical12DB;
GO

CREATE SCHEMA Test3
GO

CREATE TABLE Test3.Designation
(
    Id          INT PRIMARY KEY IDENTITY(1,1),
    DesignationName NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE Test3.Employees
(
    Id              INT PRIMARY KEY IDENTITY(1,1),
    FirstName       NVARCHAR(50)   NOT NULL,
    MiddleName      NVARCHAR(50)   NULL,
    LastName        NVARCHAR(50)   NOT NULL,
    DOB             DATE           NOT NULL,
    MobileNumber    NVARCHAR(10)   NOT NULL,
    Address         NVARCHAR(100)  NULL,
    Salary          DECIMAL(18,2)  NOT NULL,
    DesignationId   INT            NULL,

    CONSTRAINT FK_Employee_Designation 
        FOREIGN KEY (DesignationId) REFERENCES Test3.Designation(Id)
)
GO

INSERT INTO Test3.Designation (DesignationName) VALUES
('Software Engineer'),
('Senior Software Engineer'),
('Team Lead'),
('Project Manager'),
('HR Manager')
GO

INSERT INTO Test3.Employees 
    (FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId) 
VALUES
('John',    'William', 'Doe',     '2000-05-15', '9876543210', '123 Main St, New York',  55000.00, 1),
('Jane',    NULL,      'Smith',   '1999-08-22', '8765432109', NULL,                      75000.00, 3),
('Alice',   'Mary',    'Johnson', '2001-03-10', '7654321098', '456 Park Ave, London',    45000.00, 1),
('Bob',     NULL,      'Brown',   '2000-11-30', '6543210987', '789 Oak Rd, Texas',       95000.00, 4),
('Charlie', 'James',   'Wilson',  '1998-07-19', '5432109876', NULL,                      65000.00, 2)
GO
