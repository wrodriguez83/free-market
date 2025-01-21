CREATE PROCEDURE [dbo].[get_carts]
	@id int = NULL
AS
BEGIN
	SELECT 
		id, 
		name, 
		updated_at
	FROM dbo.Carts
	WHERE @id is null or id=@id
	ORDER BY name
END
