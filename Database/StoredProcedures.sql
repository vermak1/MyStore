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
