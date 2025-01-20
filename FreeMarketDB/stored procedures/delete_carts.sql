CREATE PROCEDURE [dbo].[delete_carts]
	@id int
AS
BEGIN
	BEGIN TRY
		UPDATE dbo.Carts set deleted_at = GETDATE() WHERE id=@id
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

END
