CREATE TABLE [dbo].[Bitmaps]
(
	[BitmapId] INT NOT NULL IDENTITY CONSTRAINT PK_Bitmaps PRIMARY KEY, 
    [BitmapWidth] INT NOT NULL, 
    [BitmapHeight] INT NOT NULL, 
    [BitmapSequenceId] INT NOT NULL CONSTRAINT FK_Bitmaps_BitmapSequences FOREIGN KEY REFERENCES BitmapSequences(BitmapSequenceId), 
    [BitmapIndex] INT NOT NULL,
	CONSTRAINT AK_Bitmaps_BitmapSequenceId_BitmapIndex UNIQUE (BitmapSequenceId, BitmapIndex)

)
