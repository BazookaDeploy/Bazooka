CREATE TABLE [dbo].[Deployments]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [EnviromentId] INT NOT NULL, 
    [StartDate] DATETIME NULL, 
    [Log] NVARCHAR(MAX) NOT NULL, 
    [EndDate] DATETIME NULL, 
    [Status] INT NOT NULL, 
    [Version] NCHAR(256) NOT NULL
)
