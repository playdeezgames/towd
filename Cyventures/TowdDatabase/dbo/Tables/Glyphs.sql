CREATE TABLE [dbo].[Glyphs]
(
	[GlyphId] INT NOT NULL IDENTITY CONSTRAINT PK_Glyphs PRIMARY KEY, 
    [GlyphCharacter] INT NOT NULL, 
    [FontId] INT NOT NULL CONSTRAINT FK_Glyphs_Fonts REFERENCES Fonts(FontId), 
    [GlyphWidth] INT NOT NULL,
	CONSTRAINT AK_Glyphs_FontId_GlyphName UNIQUE (FontId,[GlyphCharacter])
)
