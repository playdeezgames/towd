CREATE TABLE [dbo].[TileRoleOverrides]
(
	[TileRoleOverrideId] INT NOT NULL IDENTITY CONSTRAINT PK_TileRoleOverrides PRIMARY KEY, 
    [TileId] INT NOT NULL CONSTRAINT FK_TileRoleOverrides_Tiles FOREIGN KEY REFERENCES Tiles(TileId), 
    [TileRoleId] INT NOT NULL CONSTRAINT FK_TileRoleOverrides_TileRoles FOREIGN KEY REFERENCES TileRoles(TileRoleId),
	CONSTRAINT AK_TileRoleOverrides_TileId UNIQUE (TileId)
)
