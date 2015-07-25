CREATE TABLE [dbo].[LocalScriptTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Script] NVARCHAR(MAX) NOT NULL,
	[EnviromentId] INT NOT NULL, 
    CONSTRAINT [FK_LocalScriptTasks_ToEnviroments] FOREIGN KEY ([EnviromentId]) REFERENCES [Enviroments]([Id])
)
