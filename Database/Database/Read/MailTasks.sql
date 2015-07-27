CREATE VIEW [rd].[MailTasks]
	AS	SELECT [Id], [Name], [Text], [Recipients], [EnviromentId], [Sender] 
		FROM [dbo].MailTasks
