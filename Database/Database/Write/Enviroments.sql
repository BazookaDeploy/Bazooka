CREATE TABLE [dbo].[Enviroments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationId] INT NOT NULL, 
    [Configuration] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NOT NULL, 
    [OwnerId] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_Enviroments_Applications] FOREIGN KEY 
		([ApplicationId]) REFERENCES [Applications]([Id]),
    CONSTRAINT [FK_Enviroments_Users] FOREIGN KEY 
		([OwnerId]) REFERENCES [AspNetUsers]([Id])
)
