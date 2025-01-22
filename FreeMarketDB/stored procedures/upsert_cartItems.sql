CREATE PROCEDURE [dbo].[upsert_cartItems]
	@id int = NULL,
	@cartId int,
	@productId int,
	@quantity int,
	@price decimal
AS
BEGIN
	BEGIN TRANSACTION
	BEGIN TRY
		IF @id is null BEGIN
			INSERT INTO dbo.CartItems (productId,quantity,price,cartId) values (@productId,@quantity,@price,@cartId)
			SELECT CONVERT(int,SCOPE_IDENTITY())
		END ELSE BEGIN
			UPDATE dbo.CartItems SET productId=@productId,quantity=@quantity,price=@price,updated_at = GETDATE() WHERE id=@id
			SELECT @id
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH

END
