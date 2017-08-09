CREATE TABLE [dbo].[Deployments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EnviromentId] INT NOT NULL, 
    [StartDate] DATETIME NULL, 
    [EndDate] DATETIME NULL, 
    [Status] INT NOT NULL, 
    [Version] NCHAR(256) NULL, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [Scheduled] BIT NOT NULL DEFAULT 0, 
    [ApplicationId] INT NOT NULL,
	CONSTRAINT [FK_Deployments_ToEnviroments] FOREIGN KEY (EnviromentId) REFERENCES [Enviroments]([Id]),
	CONSTRAINT [FK_Deployments_ToApplications] FOREIGN KEY (ApplicationId) REFERENCES Applications([Id])
)

GO

CREATE INDEX [IX_Deployments_Application] ON [dbo].[Deployments] (ApplicationId,EnviromentId,StartDate)
