CREATE TABLE [dbo].[Creatures]
(
	[CreatureId] INT NOT NULL IDENTITY CONSTRAINT PK_Creatures PRIMARY KEY, 
    [BitmapId] INT NOT NULL CONSTRAINT FK_Creatures_Bitmaps FOREIGN KEY REFERENCES Bitmaps(BitmapId), 
    [CreatureName] NVARCHAR(50) NOT NULL CONSTRAINT AK_Creatures_CreatureName UNIQUE
)
