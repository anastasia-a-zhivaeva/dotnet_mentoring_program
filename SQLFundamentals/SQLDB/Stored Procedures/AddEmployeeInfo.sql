CREATE PROCEDURE [dbo].[AddEmployeeInfo]
	@EmployeeName nvarchar(100) = NULL,
	@FirstName nvarchar(50) = NULL,
	@LastName nvarchar(50) = NULL,
	@CompanyName nvarchar(50),
	@Position nvarchar(30) = NULL,
	@Street nvarchar(50),
	@City nvarchar(20) = NULL,
	@State nvarchar(50) = NULL,
	@ZipCode nvarchar(50) = NULL
AS
	SET NOCOUNT ON;  

	IF @EmployeeName IS NULL AND (@FirstName IS NULL AND @LastName IS NULL)
	BEGIN
		PRINT 'EmployeeName or FirstName and LastName must not be NULL'
		RETURN
	END

	IF @CompanyName IS NULL
	BEGIN
		PRINT 'CompanyName must not be NULL'
		RETURN
	END

	IF @Street IS NULL
	BEGIN
		PRINT 'Street must not be NULL'
		RETURN
	END

	
	DECLARE @FirstNameToInsert nvarchar(50)
	DECLARE @LastNameToInsert nvarchar(50)
	DECLARE @EmployeeNameToInsert nvarchar(100)

	IF @FirstName IS NULL AND @LastName IS NULL
	BEGIN
		DECLARE @SpaceIndex int = CHARINDEX(' ', @EmployeeName, 0)
		SELECT @FirstNameToInsert = SUBSTRING(@EmployeeName, 0, @SpaceIndex),
			@LastNameToInsert = SUBSTRING(@EmployeeName, @SpaceIndex + 1, LEN(@EmployeeName));
		SET @EmployeeNameToInsert = @EmployeeName
	END
	ELSE
	BEGIN
		SET @FirstNameToInsert = @FirstName
		SET @LastNameToInsert = @LastName
		SET @EmployeeNameToInsert = CONCAT(@FirstName, ' ', @LastName)
	END


	DECLARE @PersonIdTable TABLE (Id int)
	DECLARE @PersonId int

	INSERT INTO [dbo].[Person] (FirstName, LastName)
	OUTPUT Inserted.Id INTO @PersonIdTable
	VALUES (@FirstNameToInsert, @LastNameToInsert);

	SET @PersonId = (SELECT TOP 1 Id FROM @PersonIdTable);

	
	DECLARE @AddressIdTable TABLE (Id int)
	DECLARE @AddressId int

	INSERT INTO [dbo].[Address] (Street, City, State, ZipCode)
	OUTPUT Inserted.Id INTO @AddressIdTable
	VALUES (@Street, @City, @State, @ZipCode);

	SET @AddressId = (SELECT TOP 1 Id FROM @AddressIdTable);


	INSERT INTO [dbo].[Employee] (AddressId, PersonId, CompanyName, Position, EmployeeName)
	VALUES (@AddressId, @PersonId, SUBSTRING(@CompanyName, 0, 20), @Position, @EmployeeName);

	PRINT 'EmployeeInfo is added successfully'
GO
