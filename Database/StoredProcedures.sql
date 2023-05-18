--ListAllCars
PRINT N'Creating [dbo].[ListAllCars]';
IF EXISTS (SELECT  *
           FROM sys.objects
           WHERE object_id = OBJECT_ID(N'[dbo].[ListAllCars]')
           AND type IN ( N'P', N'PC' ))
    DROP PROCEDURE [dbo].[ListAllCars];
GO

CREATE PROCEDURE [dbo].[ListAllCars]
AS
SELECT *
    FROM [dbo].[Cars]
GO


--ListCarsByName
PRINT N'Creating [dbo].[ListCarsByName]';
IF EXISTS (SELECT  *
           FROM sys.objects
           WHERE object_id = OBJECT_ID(N'[dbo].[ListCarsByName]')
           AND type IN ( N'P', N'PC' ))
    DROP PROCEDURE [dbo].[ListCarsByName];
GO

CREATE PROCEDURE [dbo].[ListCarsByName]
    @name nvarchar(30)
AS
SELECT *
    FROM [dbo].[Cars]
    WHERE brand = @name
GO


--ListCarsByYear
PRINT N'Creating [dbo].[ListCarsByYear]';
IF EXISTS (SELECT  *
           FROM sys.objects
           WHERE object_id = OBJECT_ID(N'[dbo].[ListCarsByYear]')
           AND type IN ( N'P', N'PC' ))
    DROP PROCEDURE [dbo].[ListCarsByYear];
GO

CREATE PROCEDURE [dbo].[ListCarsByYear] 
    @year int
AS
SELECT *
    FROM [dbo].[Cars]
    WHERE year = @year
GO


--ListCarsByNameAndYear
PRINT N'Creating [dbo].[ListCarsByNameAndYear]';
IF EXISTS (SELECT  *
           FROM sys.objects
           WHERE object_id = OBJECT_ID(N'[dbo].[ListCarsByNameAndYear]')
           AND type IN ( N'P', N'PC' ))
    DROP PROCEDURE [dbo].[ListCarsByNameAndYear];
GO

CREATE PROCEDURE [dbo].[ListCarsByNameAndYear] 
    @year int,
    @name nvarchar(30)
AS
SELECT *
    FROM [dbo].[Cars]
    WHERE brand = @name AND year = @year
GO


--FindCustomerById
PRINT N'Creating [dbo].[FindCustomerById]';
IF EXISTS (SELECT  *
           FROM sys.objects
           WHERE object_id = OBJECT_ID(N'[dbo].[FindCustomerById]')
           AND type IN ( N'P', N'PC' ))
    DROP PROCEDURE [dbo].[FindCustomerById];
GO

CREATE PROCEDURE [dbo].[FindCustomerById] @id uniqueidentifier
AS
SELECT *
    FROM [dbo].[Customers]
  WHERE id = @id
GO

--FindCustomerByName
PRINT N'Creating [dbo].[FindCustomerByName]';
IF EXISTS (SELECT  *
           FROM sys.objects
           WHERE object_id = OBJECT_ID(N'[dbo].[FindCustomerByName]')
           AND type IN ( N'P', N'PC' ))
    DROP PROCEDURE [dbo].[FindCustomerByName];
GO

CREATE PROCEDURE [dbo].[FindCustomerByName]
	@firstname nvarchar(30),
	@lastname nvarchar(30)
AS
	SELECT * from [dbo].Customers
	WHERE first_name = @firstname and last_name = @lastname
GO

--CreateCustomer
PRINT N'Creating [dbo].[CreateCustomer]';
IF EXISTS (SELECT  *
           FROM sys.objects
           WHERE object_id = OBJECT_ID(N'[dbo].[CreateCustomer]')
           AND type IN ( N'P', N'PC' ))
    DROP PROCEDURE [dbo].CreateCustomer;
GO

CREATE PROCEDURE [dbo].[CreateCustomer]
	@id uniqueidentifier,
	@firstname nvarchar(30),
	@lastname nvarchar(30),
	@createddate datetime,
	@address nvarchar(50),
	@city nvarchar(30),
	@number nvarchar(20),
	@mail nvarchar(50)
AS
	INSERT INTO [dbo].Customers VALUES(
	@id, @firstname, @lastname,
	@createddate, @address, @city,
	@number, @mail)
