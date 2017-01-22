CREATE VIEW [rd].[Tasks]
	AS ( 
		SELECT Id,Name,0 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].[DeployTasks] WHERE Deleted  = 0 
		UNION
		SELECT Id,Name,1 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].MailTasks WHERE Deleted  = 0 
		UNION
		SELECT Id,Name,3 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].LocalScriptTasks WHERE Deleted  = 0 
		UNION
		SELECT Id,Name,2 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].RemoteScriptTasks WHERE Deleted  = 0 
		UNION
		SELECT Id,Name,4 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].DatabaseTasks WHERE Deleted  = 0 
	)
