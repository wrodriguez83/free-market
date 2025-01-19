CREATE TABLE [dbo].[CartItem]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [created_at] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[updated_at] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[deleted_at] DATETIME2, 
    [productId] INT NOT NULL, 
    [quantity] INT NOT NULL DEFAULT 0,
)
