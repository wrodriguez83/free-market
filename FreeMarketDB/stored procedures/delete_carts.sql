﻿CREATE PROCEDURE [dbo].[delete_carts]
	@id int
AS
BEGIN
	BEGIN TRANSACTION
	BEGIN TRY
		DELETE FROM dbo.Carts WHERE id=@id
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH

END
