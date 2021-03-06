﻿CREATE TABLE [dbo].[DeployTasksParameters]
(
	[DeployTaskId] INT NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Value] NVARCHAR(256) NOT NULL, 
    [Id] INT NOT NULL IDENTITY , 
    [Encrypted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_DeployUnitsParameters_DeployUnits] FOREIGN KEY ([DeployTaskId]) REFERENCES [DeployTasks]([Id]), 
    CONSTRAINT [PK_DeployUnitsParameters] PRIMARY KEY ([Id])
)
