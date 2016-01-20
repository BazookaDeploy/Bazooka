CREATE VIEW rd.[LocalScriptTasks]
	AS	SELECT * 
		FROM [dbo].LocalScriptTasks
		WHERE Deleted  = 0 