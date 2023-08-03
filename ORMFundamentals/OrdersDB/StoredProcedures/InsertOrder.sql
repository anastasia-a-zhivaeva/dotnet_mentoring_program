CREATE PROCEDURE [dbo].[InsertOrder]
	@Status varchar(50),
	@CreatedDate Datetime,
	@UpdatedDate Datetime,
	@ProductId int,
	@Identity int OUT
AS
INSERT INTO [dbo].[Order] (Status, CreatedDate, UpdatedDate, ProductId) VALUES(@Status, @CreatedDate, @UpdatedDate, @ProductId)
SET @Identity = SCOPE_IDENTITY()