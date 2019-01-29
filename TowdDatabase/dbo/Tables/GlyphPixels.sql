CREATE TABLE [dbo].[GlyphPixels]
(
	[GlyphPixelId] INT NOT NULL IDENTITY CONSTRAINT PK_GlyphPixels PRIMARY KEY, 
    [GlyphId] INT NOT NULL CONSTRAINT FK_GlyphPixels_Glyphs REFERENCES Glyphs(GlyphId), 
    [X] INT NOT NULL, 
    [Y] INT NOT NULL,
	CONSTRAINT AK_GlyphPixels_GlyphId_X_Y UNIQUE (GlyphId, [X], [Y])
)
