SELECT [Users].[UserId],
    [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
    [Users].[Email],
    [Users].[Gender],
    [Users].[Active],
    [UserJobInfo].[JobTitle],
    [UserJobInfo].[Department]
FROM TutorialAppSchema.Users AS Users
-- LEFT JOIN will do the regular combining of inner join BUT will also return the other values that don't meet the key condition
    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
        ON UserJobInfo.UserId = Users.UserId
WHERE Users.Active = 1
ORDER BY Users.UserId DESC

-- DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId > 500

SELECT [Users].[UserId],
    [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
    [UserJobInfo].[JobTitle],
    [UserJobInfo].[Department],
    [UserSalary].[Salary],
    [Users].[Email],
    [Users].[Gender],
    [Users].[Active]
FROM TutorialAppSchema.Users AS Users
    JOIN TutorialAppSchema.UserSalary AS UserSalary
        ON UserSalary.UserId = Users.UserId
    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
        ON UserJobInfo.UserId = Users.UserId
WHERE Users.Active = 1
ORDER BY Users.UserId DESC

-- DELETE From TutorialAppSchema.UserSalary WHERE UserId BETWEEN 250 AND 750 -- 501 Rows
-- When we use BETWEEN we also include the Lower and Upper bound of the value we're checking

-- WHERE EXISTS is used to check whether the result of a nested query is empty or not (result is boolean so it is quick, it is not returning all of the data each time to check)
-- Same as a join but faster
SELECT [UserSalary].[UserId],
    [UserSalary].[Salary]
From TutorialAppSchema.UserSalary AS UserSalary
WHERE EXISTS (
    SELECT * FROM TutorialAppSchema.UserJobInfo AS UserJobInfo
    WHERE UserJobInfo.UserId = UserSalary.UserId
) AND UserId <> 7 -- NOT equal to 7

SELECT [UserId],
[Salary] FROM TutorialAppSchema.UserSalary
-- UNION -- Distinct BETWEEN THE TWO QUERIES 
-- so if shows up in both the top and bottom dataset it will be excluded from bottom dataset but if it shows up twice in the top dataset it'll show up twice still
UNION ALL
SELECT [UserId],
[Salary] FROM TutorialAppSchema.UserSalary

SELECT [Users].[UserId],
    [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
    [UserJobInfo].[JobTitle],
    [UserJobInfo].[Department],
    [UserSalary].[Salary],
    [Users].[Email],
    [Users].[Gender],
    [Users].[Active]
FROM TutorialAppSchema.Users AS Users
    JOIN TutorialAppSchema.UserSalary AS UserSalary
        ON UserSalary.UserId = Users.UserId
    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
        ON UserJobInfo.UserId = Users.UserId
WHERE Users.Active = 1
ORDER BY Users.UserId DESC
-- Take all records that are inside this table and physically order them by that field
-- How it's physically stored is in order of this ID, so when we go look for that by ID it'll be super quick since we know the exact location of it
CREATE CLUSTERED INDEX cix_UserSalary_UserId ON TutorialAppSchema.UserSalary(UserId)


-- CREATE INDEX cix_UserSalary_UserId ON TutorialAppSchema.UserSalary(UserId)
-- same this as NONCLUSTERED

-- if we have a clustered index already, includes the fields as well as the clustered index (we might already have on like the above in line 74)
-- CREATE NONCLUSTERED INDEX ix_UserJobInfo_JobTitle ON TutorialAppSchema.UserJobInfo(JobTitle) INCLUDE (Department)
-- now we can find job title and department very quickly


-- Filtered index where it cuts out any user that isn't active
CREATE NONCLUSTERED INDEX ix_Users_Active ON TutorialAppSchema.Users(Active) 
    INCLUDE ([Email],[FirstName], [LastName]) -- Also includes UserId because it is our clustered index (but primary key is the default clustered key already though)
        WHERE Active = 1

SELECT ISNULL([UserJobInfo].[Department], 'No Department Listed') AS Department,
    SUM([UserSalary].[Salary]) AS Salary,
    MIN([UserSalary].[Salary]) AS MinSalary,
    MAX([UserSalary].[Salary]) AS MaxSalary,
    AVG([UserSalary].[Salary]) AS AvgSalary,
    COUNT(*) AS PeopleInDepartment,
    STRING_AGG(Users.UserId, ', ') AS UserIds
FROM TutorialAppSchema.Users AS Users
    JOIN TutorialAppSchema.UserSalary AS UserSalary
        ON UserSalary.UserId = Users.UserId
    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
        ON UserJobInfo.UserId = Users.UserId
WHERE Users.Active = 1
GROUP BY [UserJobInfo].[Department]
ORDER BY Department DESC

SELECT [Users].[UserId],
    [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
    [UserJobInfo].[JobTitle],
    [UserJobInfo].[Department],
    DepartmentAverage.AvgSalary,
    [UserSalary].[Salary],
    [Users].[Email],
    [Users].[Gender],
    [Users].[Active]
FROM TutorialAppSchema.Users AS Users
    JOIN TutorialAppSchema.UserSalary AS UserSalary
        ON UserSalary.UserId = Users.UserId
    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
        ON UserJobInfo.UserId = Users.UserId
    OUTER APPLY ( -- Similar to LEFT JOIN
        -- SELECT TOP 1 but kinda rendudant
        SELECT ISNULL([UserJobInfo2].[Department], 'No Department Listed') AS Department,
            AVG([UserSalary2].[Salary]) AS AvgSalary
        FROM TutorialAppSchema.UserSalary AS UserSalary2
            LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2
                ON UserJobInfo2.UserId = UserSalary2.UserId
        WHERE ISNULL([UserJobInfo2].[Department], 'No Department Listed') = ISNULL([UserJobInfo].[Department], 'No Department Listed')
        GROUP BY [UserJobInfo2].[Department]
    ) AS DepartmentAverage
WHERE Users.Active = 1
ORDER BY Users.UserId DESC