CREATE PROCEDURE [dbo].[get_carts]
	@id int = NULL
AS
BEGIN
	SELECT 
		id, 
		name, 
		updated_at, 
		CASE 
            WHEN deleted_at IS NULL THEN 1 
            ELSE 0 
        END active
	FROM dbo.Carts
	WHERE @id is null or id=@id
	ORDER BY name
END
