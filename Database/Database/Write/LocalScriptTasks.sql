CREATE TABLE [dbo].[LocalScriptTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Script] NVARCHAR(MAX) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[EnviromentId] INT NOT NULL, 
    [ApplicationId] INT NOT NULL, 
    [Position] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_LocalScriptTasks_ToEnviroments] FOREIGN KEY ([EnviromentId]) REFERENCES [Enviroments]([Id]), 
    CONSTRAINT [FK_LocalScriptTasks_ToApplications] FOREIGN KEY (ApplicationId) REFERENCES Applications([Id])
)
