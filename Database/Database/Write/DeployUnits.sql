CREATE TABLE [dbo].[DeployUnits]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[EnviromentId] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
    [Machine] NVARCHAR(50) NOT NULL, 
    [PackageName] NVARCHAR(256) NOT NULL, 
    [Directory] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_DeployUnits_Enviroments] FOREIGN KEY ([EnviromentId]) REFERENCES [Enviroments]([Id])
)
