﻿CREATE TABLE [dbo].[Tiles]
(
	[TileId] INT NOT NULL IDENTITY CONSTRAINT PK_Tiles PRIMARY KEY, 
    [RoomId] INT NOT NULL CONSTRAINT FK_Tiles_Rooms FOREIGN KEY REFERENCES Rooms(RoomId), 
    [X] INT NOT NULL, 
    [Y] INT NOT NULL, 
    [TerrainId] INT NOT NULL CONSTRAINT FK_Tiles_Terrains FOREIGN KEY REFERENCES Terrains(TerrainId),
	CONSTRAINT AK_Tiles_RoomId_X_Y UNIQUE (RoomId, X, Y)
)
