CREATE TABLE [dbo].[DeployLogs]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [DeployUnitId] INT NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [Log] NVARCHAR(MAX) NOT NULL
)
