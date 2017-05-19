CREATE TABLE [dbo].[TemplatedTask](
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[EnviromentId] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
    [AgentId] INT NOT NULL, 
    [CurrentlyDeployedVersion] NVARCHAR(50) NULL, 
    [ApplicationId] INT NOT NULL, 
    [Position] INT NOT NULL DEFAULT 1, 
    [Deleted] BIT NOT NULL DEFAULT 0, 
	TaskTemplateVersionId int NOT NULL,
    CONSTRAINT [FK_TemplatedTask_Enviroments] FOREIGN KEY ([EnviromentId]) REFERENCES [Enviroments]([Id]), 
    CONSTRAINT [FK_TemplatedTask_Applications] FOREIGN KEY (ApplicationId) REFERENCES Applications([Id]), 
    CONSTRAINT [FK_TemplatedTask_Agents] FOREIGN KEY (AgentId) REFERENCES Agents(Id),
	 CONSTRAINT [FK_TemplatedTask_TaskTemplate] FOREIGN KEY (TaskTemplateVersionId) REFERENCES TaskTemplateVersions(Id)
)
