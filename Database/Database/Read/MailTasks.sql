CREATE VIEW [rd].[MailTasks]
	AS	SELECT [Id], [Name], [Text], [Recipients], [EnviromentId],ApplicationId , [Sender] 
		FROM [dbo].MailTasks
		WHERE Deleted  = 0 
