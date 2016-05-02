CREATE TABLE [dbo].[DeploymentTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[DeploymentId] INT NOT NULL,
	[DeployTaskId] INT NOT NULL,
	[DeployType] INT NOT NULL, 
    CONSTRAINT [FK_DeploymentTasks_ToDeployments] FOREIGN KEY ([DeploymentId]) REFERENCES [Deployments]([Id])
)
