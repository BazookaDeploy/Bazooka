CREATE VIEW [dbo].[Tasks]
	AS ( 
		SELECT Id,Name,0 AS Type FROM [dbo].[DeployTasks]
		UNION
		SELECT Id,Name,1 AS Type FROM [dbo].MailTasks
		UNION
		SELECT Id,Name,2 AS Type FROM [dbo].LocalScriptTasks
		UNION
		SELECT Id,Name,3 AS Type FROM [dbo].RemoteScriptTasks
	)
