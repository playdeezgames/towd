﻿CREATE TABLE [dbo].[TileRoles]
(
	[TileRoleId] INT NOT NULL CONSTRAINT PK_TileRoles PRIMARY KEY, 
    [TileRoleName] NVARCHAR(50) NULL CONSTRAINT AK_TileRoles_TileRoleName UNIQUE
)
