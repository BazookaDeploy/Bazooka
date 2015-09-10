CREATE VIEW [rd].[DeployTasks]
	AS SELECT [dbo].DeployTasks.*, Applications.Name AS ApplicationName, Enviroments.Name as EnviromentName
	FROM [dbo].[DeployTasks] JOIN  [dbo].Enviroments ON [DeployTasks].EnviromentId = Enviroments.Id
						     JOIN  [dbo].Applications ON [DeployTasks].ApplicationId = Applications.Id
