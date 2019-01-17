CREATE TABLE [dbo].[Bitmaps]
(
	[BitmapId] INT NOT NULL IDENTITY CONSTRAINT PK_Bitmaps PRIMARY KEY, 
    [BitmapWidth] INT NOT NULL, 
    [BitmapHeight] INT NOT NULL
)
