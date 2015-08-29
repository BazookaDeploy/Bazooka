CREATE TABLE [dbo].[DatabaseTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] nvarchar(50) NOT NULL,
	[ConnectionString] NVARcHAR(256) NOT NULL,
	[Package] NVARCHAR(256) NOT NULL,
	[DatabaseName] NVARcHAR(50) NOT NULL,
	EnviromentId int NOT NULL, 
    [Repository] NVARCHAR(256) NOT NULL, 
    CONSTRAINT [FK_DatabaseTasks_ToEnviroments] FOREIGN KEY (EnviromentId) REFERENCES [Enviroments]([Id])
)
