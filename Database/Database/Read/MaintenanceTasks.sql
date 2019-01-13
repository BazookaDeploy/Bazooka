CREATE VIEW rd.[MaintenanceTasks]
	AS SELECT t.[Id], [AgentId], [TemplatedTaskId], [StartDate], [Status], [UserId] , tt.Name as TaskName,a.Name as Agent, u.UserName as UserName
	FROM dbo.MaintenanceTasks t inner join dbo.Agents a on t.AgentId = a.Id
								inner join dbo.TaskTemplate tt on t.TemplatedTaskId = tt.Id
								inner join dbo.AspNetUsers u on t.UserId = u.Id
