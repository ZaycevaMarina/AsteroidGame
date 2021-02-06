--CREATE TABLE[dbo].[Employee] (
--                                    [Id] INT IDENTITY(1, 1) NOT NULL,
--                                    [Name] NVARCHAR(MAX) COLLATE Cyrillic_General_CI_AS NOT NULL,
--                                    [Age] INTEGER NOT NULL,
--                                    [Salary] FLOAT NOT NULL,
--                                    CONSTRAINT[PK_dbo.Employee] PRIMARY KEY CLUSTERED([Id] ASC)
--                                );

--INSERT INTO [dbo].[Employee] (Name, Age, Salary) VALUES (N'Вася', 22, 3000);
--INSERT INTO [dbo].[Employee] (Name, Age, Salary) VALUES (N'Петя', 25, 6000);
--INSERT INTO [dbo].[Employee] (Name, Age, Salary) VALUES (N'Коля', 23, 8000);

--CREATE TABLE[dbo].[Department_1] (
--                                    [Id] INT NOT NULL,
--                                    CONSTRAINT[PK_dbo.Department_1] PRIMARY KEY CLUSTERED([Id] ASC)
--                                );
--SELECT * from Employee
--INSERT INTO [dbo].[Department_1] (Id) VALUES (9);
--INSERT INTO [dbo].[Department_1] (Id) VALUES (10);
--delete FROM [dbo].[Employee] where id = 13
SELECT * from Employee
SELECT * from Department_1
--CREATE TABLE[dbo].[Department_2] (
--                                    [Id] INT NOT NULL,
--                                    CONSTRAINT[PK_dbo.Department_2] PRIMARY KEY CLUSTERED([Id] ASC)
--                                );
--INSERT INTO [dbo].[Department_2] (Id) VALUES (7);
--INSERT INTO [dbo].[Department_2] (Id) VALUES (8);

SELECT * from Department_2

--INSERT INTO Department_2 (Id) VALUES ()
--delete FROM [dbo].[Employee]

--SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_TYPE LIKE '%TABLE%'

