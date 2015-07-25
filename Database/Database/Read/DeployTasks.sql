CREATE VIEW [rd].[DeployTasks]
	AS SELECT [dbo].DeployTasks.*, Applications.Name AS ApplicationName, Enviroments.Configuration as EnviromentName
	FROM [dbo].[DeployTasks] JOIN  [dbo].Enviroments ON [DeployTasks].EnviromentId = Enviroments.Id
						   JOIN  [dbo].Applications ON Enviroments.ApplicationId = Applications.Id
