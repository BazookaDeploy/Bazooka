CREATE VIEW rd.[TemplatedTaskParameters]
	AS SELECT TemplatedTaskParameters.[Id], [TemplatedTaskId], [TaskTemplateParameterId], [Value], Name, Optional , Encrypted, dbo.TaskTemplateParameters.Description FROM dbo.TemplatedTaskParameters INNER JOIN dbo.TaskTemplateParameters on [TaskTemplateParameterId] = TaskTemplateParameters.Id
