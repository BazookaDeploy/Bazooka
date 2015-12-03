CREATE TABLE [dbo].[DeployTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[EnviromentId] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
    [AgentId] INT NOT NULL, 
    [PackageName] NVARCHAR(256) NOT NULL, 
    [Directory] NVARCHAR(MAX) NOT NULL, 
    [Repository] NVARCHAR(256) NOT NULL, 
    [CurrentlyDeployedVersion] NVARCHAR(50) NULL, 
    [InstallScript] NVARCHAR(MAX) NULL, 
    [UninstallScript] NVARCHAR(MAX) NULL, 
    [ConfigurationFile] NVARCHAR(50) NULL, 
    [ConfigurationTransform] NVARCHAR(MAX) NULL, 
    [Configuration] NVARCHAR(50) NULL, 
    [ApplicationId] INT NOT NULL, 
    [Position] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_DeployUnits_Enviroments] FOREIGN KEY ([EnviromentId]) REFERENCES [Enviroments]([Id]), 
    CONSTRAINT [FK_DeployUnits_Applications] FOREIGN KEY (ApplicationId) REFERENCES Applications([Id]), 
    CONSTRAINT [FK_DeployTasks_Agents] FOREIGN KEY (AgentId) REFERENCES Agents(Id)
)
