CREATE TABLE [dbo].[WorldCreatures]
(
	[WorldCreatureId] INT NOT NULL IDENTITY CONSTRAINT PK_WorldCreatures PRIMARY KEY, 
    [WorldId] INT NOT NULL CONSTRAINT FK_WorldCreatures_Worlds FOREIGN KEY REFERENCES Worlds(WorldId), 
    [CreatureId] INT NOT NULL CONSTRAINT FK_WorldCreatures_Creatures FOREIGN KEY REFERENCES Creatures(CreatureId),
	CONSTRAINT AK_WorldCreatures_WorldId_CreatureId UNIQUE(WorldId, CreatureId)
)
