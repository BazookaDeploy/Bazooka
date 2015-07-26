CREATE VIEW [rd].[Tasks]
	AS ( 
		SELECT Id,Name,0 AS Type, EnviromentId FROM [dbo].[DeployTasks]
		UNION
		SELECT Id,Name,1 AS Type, EnviromentId FROM [dbo].MailTasks
		UNION
		SELECT Id,Name,2 AS Type, EnviromentId FROM [dbo].LocalScriptTasks
		UNION
		SELECT Id,Name,3 AS Type, EnviromentId FROM [dbo].RemoteScriptTasks
	)
