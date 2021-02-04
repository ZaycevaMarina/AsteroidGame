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

--delete FROM [dbo].[Employee]

SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_TYPE LIKE '%TABLE%'
