﻿CREATE PROCEDURE [dbo].[upsert_carts]
	@id int = NULL,
	@name varchar(max)
AS
BEGIN
	BEGIN TRY
		IF @id is null BEGIN
			INSERT INTO dbo.carts (name) values (@name)
		END ELSE BEGIN
			UPDATE dbo.carts SET name=@name, updated_at = GETDATE() WHERE id=@id
		END
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

END
