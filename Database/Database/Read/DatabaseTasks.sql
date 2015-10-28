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
			  [ApplicationId] 
		FROM dbo.DatabaseTasks JOIN dbo.Agents ON AgentId = Agents.Id
