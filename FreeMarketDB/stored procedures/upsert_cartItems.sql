CREATE PROCEDURE [dbo].[upsert_cartItems]
	@id int = NULL,
	@cartId int,
	@productId int,
	@quantity int,
	@price decimal
AS
BEGIN
	BEGIN TRY
		IF @id is null BEGIN
			INSERT INTO dbo.CartItems (productId,quantity,price,cartId) values (@productId,@quantity,@price,@cartId)
		END ELSE BEGIN
			UPDATE dbo.CartItems SET productId=@productId,quantity=@quantity,price=@price,updated_at = GETDATE() WHERE id=@id
		END
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

END
