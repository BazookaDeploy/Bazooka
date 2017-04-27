CREATE TABLE [dbo].[TaskTemplateVersions]
(
	[Id] INT NOT NULL PRIMARY KEY,
	TaskTemplateId INT NOT NULL,
	
	Script NVARCHAR(MAX) NOT NULL, 
    [Version] INT NOT NULL, 
    CONSTRAINT [FK_TaskTemplateVersions_TaskTemplate] FOREIGN KEY (TaskTemplateId) REFERENCES TaskTemplate(Id)
)
