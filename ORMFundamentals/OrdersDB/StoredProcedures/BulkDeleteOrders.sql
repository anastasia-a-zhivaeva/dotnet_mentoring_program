CREATE PROCEDURE [dbo].[BulkDeleteOrders]
	@Month int = NULL,
	@Year int = NULL,
	@Status varchar(50) = NULL,
	@ProductId int = NULL
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	SAVE TRANSACTION BulkDeleteSavePoint;

	BEGIN TRY

		DELETE from [dbo].[Order]
		OUTPUT Deleted.*
		WHERE 
			(MONTH(CreatedDate) = @Month)
		OR (YEAR(CreatedDate) = @Year)
		OR (Status LIKE @Status)
		OR (ProductId = @ProductId);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
	IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION BulkDeleteSavePoint;
        END
    END CATCH
END
GO