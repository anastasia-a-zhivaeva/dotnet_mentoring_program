/*'
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO Person (FirstName, LastName)
VALUES ('Meredith', 'Grey'), ('Derek', 'Shepard'), ('Cristina', 'Yang'), ('Erica', 'Hahn'), ('Jackson', 'Avery'), ('April', 'Kepner');

INSERT INTO Address (Street, City, State, ZipCode)
VALUES ('15000 Centennial Drive', 'Seattle', 'Washington', '98109'), ('314 Barrington Ave.', 'Seattle', 'Washington', '98103');

INSERT INTO Company (Name, AddressId)
VALUES ('Mercy West Hospital', 1), ('Seattle Grace Hospital', 2);

INSERT INTO Employee (AddressId, PersonId, CompanyName, Position, EmployeeName)
VALUES (2, 1, 'Seattle Grace Hospital', 'Attendant', 'Meredith Grey'),
       (2, 2, 'Seattle Grace Hospital', 'Resident', 'Derek Shepard'),
       (2, 3, 'Seattle Grace Hospital', 'Attendant', 'Cristina Yang'),
       (1, 4, 'Mercy West Hospital', 'Resident', 'Erica Hahn'),
       (1, 5, 'Mercy West Hospital', 'Attendant', 'Jackson Avery'),
       (1, 6, 'Mercy West Hospital', 'Attendant', 'April Kepner');