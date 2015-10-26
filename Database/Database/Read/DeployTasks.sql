CREATE VIEW [rd].[DeployTasks]
	AS SELECT [dbo].[DeployTasks].[Id], 
		      [dbo].[DeployTasks].[EnviromentId], 
			  [dbo].[DeployTasks].[Name], 
			  [dbo].[DeployTasks].[AgentId], 
			  [dbo].Agents.Address,
			  [dbo].Agents.Name as AgentName,
			  [dbo].[DeployTasks].[PackageName], 
			  [dbo].[DeployTasks].[Directory], 
			  [dbo].[DeployTasks].[Repository],
			  [dbo].[DeployTasks].[CurrentlyDeployedVersion], 
			  [dbo].[DeployTasks].[InstallScript], 
			  [dbo].[DeployTasks].[UninstallScript], 
			  [dbo].[DeployTasks].[ConfigurationFile], 
			  [dbo].[DeployTasks].[ConfigurationTransform], 
			  [dbo].[DeployTasks].[Configuration], 
			  [dbo].[DeployTasks].[ApplicationId], 
			  Applications.Name AS ApplicationName,
			  ApplicationGroups.Name AS GroupName,
			  Enviroments.Name as EnviromentName
	FROM [dbo].[DeployTasks] JOIN  [dbo].Enviroments ON [DeployTasks].EnviromentId = Enviroments.Id
						     JOIN  [dbo].Applications ON [DeployTasks].ApplicationId = Applications.Id
							 JOIN  [dbo].Agents on [DeployTasks].AgentId = Agents.Id
							 LEFT JOIN dbo.ApplicationGroups ON Applications.ApplicationGroupId = ApplicationGroups.Id
