CREATE VIEW rd.[RemoteScriptTasks]
	AS SELECT [RemoteScriptTasks].[Id], 
			  [Script], 
			  AgentId, 
			  Agents.Name AS AgentName, 
			  Agents.[Address], 
			  [RemoteScriptTasks].[Name], 
			  [RemoteScriptTasks].[EnviromentId],
			  [RemoteScriptTasks].ApplicationId ,
			  Folder 
	FROM [dbo].RemoteScriptTasks JOIN dbo.Agents ON AgentId = Agents.Id
	WHERE Deleted  = 0 
