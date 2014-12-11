CREATE TABLE [dbo].[Enviroments]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ApplicationId] INT NOT NULL, 
    [Configuration] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Enviroments_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [Applications]([Id])
)
