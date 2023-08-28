CREATE PROCEDURE [dbo].[FetchOrders]
	@Month int = NULL,
	@Year int = NULL,
	@Status varchar(50) = NULL,
	@ProductId int = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, Status, CreatedDate, UpdatedDate, ProductId from [dbo].[Order]
	WHERE 
		(@Month IS NULL OR MONTH(CreatedDate) = @Month)
	AND (@Year IS NULL OR YEAR(CreatedDate) = @Year)
	AND (@Status IS NULL OR Status LIKE @Status)
	AND (@ProductId IS NULL OR ProductId = @ProductId);
END
GO
