CREATE TABLE [dbo].[CartItems]
(
	[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [created_at] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[updated_at] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [productId] INT NOT NULL, 
    [quantity] INT NOT NULL DEFAULT 0,
    [price] DECIMAL NOT NULL DEFAULT 0,
    [cartId] INT NOT NULL REFERENCES dbo.Carts([id]),
)
