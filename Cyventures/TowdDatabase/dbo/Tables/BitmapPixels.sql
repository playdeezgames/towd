CREATE TABLE [dbo].[BitmapPixels]
(
	[BitmapPixelId] INT NOT NULL IDENTITY CONSTRAINT PK_BitmapPixels PRIMARY KEY, 
    [X] INT NOT NULL, 
    [Y] INT NOT NULL, 
    [ColorId] INT NOT NULL CONSTRAINT FK_BitmapPixels_Colors REFERENCES Colors(ColorId), 
    [BitmapId] INT NOT NULL CONSTRAINT FK_BitmapPixel_Bitmaps REFERENCES Bitmaps(BitmapId),
    CONSTRAINT AK_BitmapPixels_BitmapId_X_Y UNIQUE(BitmapId,X,Y)
)
