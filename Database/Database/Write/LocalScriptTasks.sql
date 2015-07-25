CREATE TABLE [dbo].[LocalScriptTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Script] NVARCHAR(MAX) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[EnviromentId] INT NOT NULL, 
    CONSTRAINT [FK_LocalScriptTasks_ToEnviroments] FOREIGN KEY ([EnviromentId]) REFERENCES [Enviroments]([Id])
)
