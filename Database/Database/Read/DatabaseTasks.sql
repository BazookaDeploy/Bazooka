CREATE VIEW [rd].[DatabaseTasks]
	AS SELECT DatabaseTasks.[Id], 
			  DatabaseTasks.[Name], 
			  [ConnectionString], 
			  [Package], 
			  [DatabaseName], 
			  DatabaseTasks.[EnviromentId], 
			  [Repository], 
			  [AgentId],
			  Agents.Name as AgentName, 
			  [ApplicationId] ,
			  Partial
		FROM dbo.DatabaseTasks JOIN dbo.Agents ON AgentId = Agents.Id
		WHERE Deleted  = 0 
