CREATE TABLE [dbo].[WorldTerrains]
(
	[WorldTerrainId] INT NOT NULL IDENTITY CONSTRAINT PK_WorldTerrains PRIMARY KEY, 
    [WorldId] INT NOT NULL CONSTRAINT FK_WorldTerrains_Worlds FOREIGN KEY REFERENCES Worlds(WorldId), 
    [TerrainId] INT NOT NULL CONSTRAINT FK_WorldTerrains_Terrains FOREIGN KEY REFERENCES Terrains(TerrainId),
	CONSTRAINT AK_WorldTerrains_WorldId_TerrainId UNIQUE (WorldId, TerrainId)
)
