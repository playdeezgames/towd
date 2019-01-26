CREATE TABLE [dbo].[CreatureInstances]
(
	[CreatureInstanceId] INT NOT NULL IDENTITY CONSTRAINT PK_CreatureInstances PRIMARY KEY, 
    [CreatureId] INT NOT NULL CONSTRAINT FK_CreatureInstances_Creatures FOREIGN KEY REFERENCES Creatures(CreatureId), 
    [TileId] INT NOT NULL CONSTRAINT FK_CreatureInstances_Tiles FOREIGN KEY REFERENCES Tiles(TileId),
	CONSTRAINT AK_CreatureInstances_TileId UNIQUE (TileId)
)
