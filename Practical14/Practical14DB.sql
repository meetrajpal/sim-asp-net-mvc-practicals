CREATE DATABASE Practical14DB;
GO

USE Practical14DB;
GO

CREATE TABLE Employee
(
    Id   INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    DOB  DATE        NOT NULL,
    Age  INT         NULL
)
GO

