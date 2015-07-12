CREATE TABLE [dbo].[Deployments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EnviromentId] INT NOT NULL, 
    [StartDate] DATETIME NULL, 
    [EndDate] DATETIME NULL, 
    [Status] INT NOT NULL, 
    [Version] NCHAR(256) NULL, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [Scheduled] BIT NOT NULL DEFAULT 0
)
