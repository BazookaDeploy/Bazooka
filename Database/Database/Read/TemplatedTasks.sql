CREATE VIEW rd.[TemplatedTasks]
	AS SELECT T.[Id], T.[EnviromentId], T.[Name], [AgentId], [CurrentlyDeployedVersion], [ApplicationId], [Position], T.[Deleted], [TaskTemplateVersionId], Agents.Address as AgentName,
	(Select max(version) from  dbo.TaskTemplateVersions AS TTV2 where TTV2.TaskTemplateId = TT2.Id) as LastKnownVersion, TTV.Script, PackageName, Repository,TT2.Id as TaskTemplateId,
			  Applications.Name AS ApplicationName,
			  ApplicationGroups.Name AS GroupName,
			  Enviroments.Name as EnviromentName
	FROM dbo.TemplatedTask as T INNER JOIN dbo.TaskTemplateVersions as TTV on T.TaskTemplateVersionId = TTV.Id
								INNER JOIN dbo.TaskTemplate AS TT2 on TT2.Id = ttv.TaskTemplateId
								INNER JOIN dbo.Agents ON AgentId = Agents.Id
								JOIN  [dbo].Enviroments ON T.EnviromentId = Enviroments.Id
						     JOIN  [dbo].Applications ON T.ApplicationId = Applications.Id
							 LEFT JOIN dbo.ApplicationGroups ON Applications.ApplicationGroupId = ApplicationGroups.Id
		WHERE T.Deleted  = 0 

