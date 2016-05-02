CREATE VIEW [rd].[DeploymentTasks]
	AS ( 
		SELECT DeploymentTasks.Id, DeploymentTasks.DeployTaskId, DeploymentTasks.DeployType, DeployTasks.Name,  DeploymentTasks.DeploymentId
			FROM [dbo].DeploymentTasks JOIN [dbo].DeployTasks 
				 ON [dbo].DeploymentTasks.DeployTaskId = [dbo].DeployTasks.Id
			WHERE DeployType  = 0 
		UNION
		SELECT DeploymentTasks.Id, DeploymentTasks.DeployTaskId, DeploymentTasks.DeployType, MailTasks.Name,  DeploymentTasks.DeploymentId
			FROM [dbo].DeploymentTasks JOIN [dbo].MailTasks 
				 ON [dbo].DeploymentTasks.DeployTaskId = [dbo].MailTasks.Id
			WHERE DeployType  = 1 
		UNION
		SELECT DeploymentTasks.Id, DeploymentTasks.DeployTaskId, DeploymentTasks.DeployType, LocalScriptTasks.Name,  DeploymentTasks.DeploymentId
			FROM [dbo].DeploymentTasks JOIN [dbo].LocalScriptTasks 
				 ON [dbo].DeploymentTasks.DeployTaskId = [dbo].LocalScriptTasks.Id
			WHERE DeployType  = 2 
		UNION
		SELECT DeploymentTasks.Id, DeploymentTasks.DeployTaskId, DeploymentTasks.DeployType, RemoteScriptTasks.Name,  DeploymentTasks.DeploymentId
			FROM [dbo].DeploymentTasks JOIN [dbo].RemoteScriptTasks 
				 ON [dbo].DeploymentTasks.DeployTaskId = [dbo].RemoteScriptTasks.Id
			WHERE DeployType  = 3  
		UNION
		SELECT DeploymentTasks.Id, DeploymentTasks.DeployTaskId, DeploymentTasks.DeployType, DatabaseTasks.Name,  DeploymentTasks.DeploymentId
			FROM [dbo].DeploymentTasks JOIN [dbo].DatabaseTasks 
				 ON [dbo].DeploymentTasks.DeployTaskId = [dbo].DatabaseTasks.Id
			WHERE DeployType  = 4  
	)
