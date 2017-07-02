CREATE VIEW rd.[TemplatedTasks]
	AS SELECT T.[Id], T.[EnviromentId], T.[Name], [AgentId], [CurrentlyDeployedVersion], [ApplicationId], [Position], [Deleted], [TaskTemplateVersionId], Agents.Address as AgentName,
	(Select max(version) from  dbo.TaskTemplateVersions AS TTV2 where TTV2.TaskTemplateId = TT2.Id) as LastKnownVersion, TTV.Script
	FROM dbo.TemplatedTask as T INNER JOIN dbo.TaskTemplateVersions as TTV on T.TaskTemplateVersionId = TTV.Id
								INNER JOIN dbo.TaskTemplate AS TT2 on TT2.Id = ttv.TaskTemplateId
								INNER JOIN dbo.Agents ON AgentId = Agents.Id
		WHERE Deleted  = 0 

