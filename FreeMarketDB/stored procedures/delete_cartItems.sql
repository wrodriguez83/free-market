CREATE PROCEDURE [dbo].[delete_cartItems]
	@idCart int
AS
BEGIN
	BEGIN TRY
		DELETE FROM dbo.CartItems WHERE cartId=@idCart
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

END
