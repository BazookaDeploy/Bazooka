CREATE VIEW rd.[TaskTemplate]
	AS SELECT dbo.TaskTemplate.[Id], [Name], [Description],  dbo.TaskTemplateVersions.[Version], [Script] 
	FROM dbo.TaskTemplate JOIN 
		 dbo.TaskTemplateVersions ON 
		 dbo.TaskTemplate.Id = dbo.TaskTemplateVersions.TaskTemplateId
