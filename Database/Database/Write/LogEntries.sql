CREATE TABLE [dbo].[LogEntries]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TimeStamp] DATETIME NOT NULL, 
    [Text] NVARCHAR(MAX) NOT NULL, 
    [Error] BIT NOT NULL, 
    [DeploymentId] INT NOT NULL, 
    CONSTRAINT [FK_LogEntries_Deployments] FOREIGN KEY ([DeploymentId]) REFERENCES [Deployments]([Id])
)
