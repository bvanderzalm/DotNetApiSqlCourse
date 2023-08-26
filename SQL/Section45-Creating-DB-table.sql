-- CREATE DATABASE DotNetCourseDatabase

-- USE DotNetCourseDatabase
-- GO

-- CREATE SCHEMA TutorialAppSchema
-- GO

-- CREATE TABLE TutorialAppSchema.Computer
-- (
--     -- TableId INT IDENTITY(Starting, Increment By) what to start at and what to increment by (if we have numeric ids)
--     ComputerId INT IDENTITY(1, 1) PRIMARY KEY
--     , Motherboard NVARCHAR(50) -- N is for non unicode characters (symbols and weird characters) vs just regular VARCHAR (VARCHAR is 1 byte where NVARCHAR is 2 bytes)
--     , CpuCores INT
--     , HasWifi BIT
--     , HasLTE BIT
--     , ReleaseDate DATETIME -- other type is DATE (no time) DATETIME includes seconds, DATETIME2 includes milliseconds
--     , Price DECIMAL(18, 4) -- 18 whole number values and 4 digits after the decimal
--     , VideoCard NVARCHAR(50)
-- )
-- GO


-- SET IDENTITY_INSERT TutorialAppSchema.Computer ON -- how to set manual ID where it doesn't auto increment

-- INSERT TOP(1) INTO TutorialAppSchema.Computer (
--     [Motherboard],
--     [CpuCores],
--     [HasWifi],
--     [HasLTE],
--     [ReleaseDate],
--     [Price],
--     [VideoCard]
-- ) VALUES (
--     'Sample-Motherboard',
--     4,
--     1,
--     0,
--     '2022-01-01',
--     1000,
--     'Sample-Videocard'
-- )

-- SET IDENTITY_INSERT TutorialAppSchema.Computer OFF

-- DELETE TOP(1) FROM TutorialAppSchema.Computer WHERE ComputerId = 101

-- Select statement that also updates? Checks if CPU core is null and if so sets to 0. The column name is also lost so set AS CpuCores as the column name
-- SELECT [ComputerId]
--       , [Motherboard]
--       , ISNULL([CPUCores], 0) AS CpuCores
--       , [HasWifi]
--       , [HasLTE]
--       , [ReleaseDate]
--       , [Price]
--       , [VideoCard]
-- FROM TutorialAppSchema.Computer

-- UPDATE TutorialAppSchema.Computer SET CpuCores = NULL WHERE [ReleaseDate] > '2017-01-01'

SELECT [ComputerId]
      , [Motherboard]
      , ISNULL([CPUCores], 4) AS CpuCores
      , [HasWifi]
      , [HasLTE]
      , [ReleaseDate]
      , [Price]
      , [VideoCard]
FROM TutorialAppSchema.Computer
ORDER By HasWifi DESC, ReleaseDate DESC -- Has wifi is the first field mentioned it's being sorted first and then relase date