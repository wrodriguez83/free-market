CREATE PROCEDURE [dbo].[get_cartItems]
	@cartId int = NULL
AS
BEGIN
	SELECT 
		id,
		cartId,
		productId, 
		quantity, 
		price,
		updated_at
	FROM dbo.CartItems
	WHERE @cartId is null or cartId=@cartId
	ORDER BY cartId
END
