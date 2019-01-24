CREATE TABLE [dbo].[Terrains]
(
	[TerrainId] INT NOT NULL IDENTITY CONSTRAINT PK_Terrains PRIMARY KEY, 
    [BitmapId] INT NOT NULL CONSTRAINT FK_Terrains_Bitmaps FOREIGN KEY REFERENCES Bitmaps(BitmapId), 
    [TerrainName] NVARCHAR(50) NOT NULL CONSTRAINT AK_Terrains_TerrainName UNIQUE, 
    [TileRoleId] INT NOT NULL CONSTRAINT FK_Terrains_TileRoles FOREIGN KEY REFERENCES TileRoles(TileRoleId) 
)
