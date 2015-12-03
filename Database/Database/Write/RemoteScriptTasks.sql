CREATE TABLE [dbo].[RemoteScriptTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Script] NVARCHAR(MAX) NOT NULL,	
    [AgentId] INT NOT NULL,	
	[Name] NVARCHAR(50) NOT NULL,
	[EnviromentId] INT NOT NULL, 
    [Folder] NVARCHAR(256) NOT NULL, 
    [ApplicationId] INT NOT NULL, 
    [Position] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_RemoteScriptTasks_ToEnviroments] FOREIGN KEY ([EnviromentId]) REFERENCES [Enviroments]([Id]), 
    CONSTRAINT [FK_RemoteScriptTasks_ToApplications] FOREIGN KEY (ApplicationId) REFERENCES Applications([Id]), 
    CONSTRAINT [FK_RemoteScriptTasks_Agents] FOREIGN KEY (AgentId) REFERENCES Agents(Id)
)
