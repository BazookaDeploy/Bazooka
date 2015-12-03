CREATE TABLE [dbo].[DatabaseTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] nvarchar(50) NOT NULL,
	[ConnectionString] NVARcHAR(256) NOT NULL,
	[Package] NVARCHAR(256) NOT NULL,
	[DatabaseName] NVARcHAR(50) NOT NULL,
	EnviromentId int NOT NULL, 
    [Repository] NVARCHAR(256) NOT NULL, 
    [AgentId] INT NOT NULL, 
    [ApplicationId] INT NOT NULL, 
    [Position] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_DatabaseTasks_ToEnviroments] FOREIGN KEY (EnviromentId) REFERENCES [Enviroments]([Id]),
	CONSTRAINT [FK_DatabaseTasks_ToApplications] FOREIGN KEY (ApplicationId) REFERENCES Applications([Id]), 
    CONSTRAINT [FK_DatabaseTasks_ToAgents] FOREIGN KEY ([AgentId]) REFERENCES [Agents]([Id])
)
