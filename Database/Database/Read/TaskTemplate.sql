CREATE VIEW rd.[TaskTemplate]
	AS SELECT dbo.TaskTemplate.[Id], [Name], [Description]
	FROM dbo.TaskTemplate 
	where Deleted=0
