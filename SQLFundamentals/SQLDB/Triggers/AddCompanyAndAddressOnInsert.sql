CREATE TRIGGER [AddCompanyAndAddressOnInsert]
	ON [dbo].[Employee]
	FOR INSERT
	AS
	BEGIN
		SET NOCOUNT ON;

		INSERT INTO [dbo].[Company] (Name, AddressId)
		SELECT CompanyName, AddressId FROM Inserted;
	END
