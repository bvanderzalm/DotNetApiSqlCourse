CREATE DATABASE DotNetCourseDatabase

USE DotNetCourseDatabase
GO

CREATE SCHEMA TutorialAppSchema
GO

CREATE TABLE TutorialAppSchema.Computer
(
    -- TableId INT IDENTITY(Starting, Increment By) what to start at and what to increment by (if we have numeric ids)
    ComputerId INT IDENTITY(1, 1) PRIMARY KEY
    , Motherboard NVARCHAR(50) -- N is for non unicode characters (symbols and weird characters) vs just regular VARCHAR (VARCHAR is 1 byte where NVARCHAR is 2 bytes)
    , CpuCores INT
    , HasWifi BIT
    , HasLTE BIT
    , Price DECIMAL(18, 4) -- 18 whole number values and 4 digits after the decimal
    , VideoCard NVARCHAR(50)
)
GO

SELECT *
FROM TutorialAppSchema.Computer
WHERE 1 = 0
