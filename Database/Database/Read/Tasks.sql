CREATE VIEW [rd].[Tasks]
	AS ( 
		SELECT Id,Name,0 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].[DeployTasks]
		UNION
		SELECT Id,Name,1 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].MailTasks
		UNION
		SELECT Id,Name,2 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].LocalScriptTasks
		UNION
		SELECT Id,Name,3 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].RemoteScriptTasks
		UNION
		SELECT Id,Name,4 AS Type, EnviromentId,ApplicationId, Position FROM [dbo].DatabaseTasks
	)
