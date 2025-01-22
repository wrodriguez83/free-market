CREATE PROCEDURE [dbo].[upsert_carts]
	@id int = NULL,
	@name varchar(max)
AS
BEGIN
	BEGIN TRANSACTION
	BEGIN TRY
		IF @id is null BEGIN
			INSERT INTO dbo.Carts (name) values (@name)
			SELECT CONVERT(int,SCOPE_IDENTITY())
		END ELSE BEGIN
			UPDATE dbo.Carts SET name=@name, updated_at = GETDATE() WHERE id=@id
			SELECT @id
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH

END
