CREATE VIEW [dbo].[EmployeeInfo]
	AS SELECT 
			Employee.Id AS EmployeeId, 
			(CASE WHEN Employee.EmployeeName IS NOT NULL THEN Employee.EmployeeName ELSE CONCAT(Person.FirstName, ' ', Person.LastName) END) AS EmployeeFullName,
			CONCAT(Address.ZipCode, '_', Address.State, ', ', Address.City, '-', Address.Street) AS EmployeeFullAddress,
			CONCAT(Employee.CompanyName, '(', Employee.Position, ')') AS EmployeeCompanyInfo
	   FROM [Employee]
	   INNER JOIN [Address] ON Address.Id = Employee.AddressId
	   INNER JOIN [Person] ON Person.Id = Employee.PersonId;
