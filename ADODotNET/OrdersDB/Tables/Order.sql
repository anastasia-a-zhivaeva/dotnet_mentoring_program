CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Status] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    [ProductId] INT NOT NULL, 
    CONSTRAINT [FK_Order_ToProduct] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]) ON DELETE CASCADE
)
