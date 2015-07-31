CREATE TABLE [dbo].[RemoteScriptTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Script] NVARCHAR(MAX) NOT NULL,	
    [Machine] NVARCHAR(50) NOT NULL,	
	[Name] NVARCHAR(50) NOT NULL,
	[EnviromentId] INT NOT NULL, 
    [Folder] NVARCHAR(256) NOT NULL, 
    CONSTRAINT [FK_RemoteScriptTasks_ToEnviroments] FOREIGN KEY ([EnviromentId]) REFERENCES [Enviroments]([Id])
)
