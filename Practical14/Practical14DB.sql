create database Practical14DB;
go

use Practical14DB;
go

CREATE TABLE Employee
(
    Id   INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    DOB  DATE        NOT NULL,
    Age  INT         NULL
)
go

