CREATE TABLE [dbo].[BitmapSequences]
(
	[BitmapSequenceId] INT NOT NULL IDENTITY CONSTRAINT PK_BitmapSequences PRIMARY KEY, 
    [BitmapSequenceName] NVARCHAR(50) NOT NULL CONSTRAINT  AK_BitmapSequences_BitmapSequenceName UNIQUE
)
