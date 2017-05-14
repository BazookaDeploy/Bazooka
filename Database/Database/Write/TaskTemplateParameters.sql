CREATE TABLE [dbo].[TaskTemplateParameters]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	TaskTemplateVersionId INT NOT NULL,
	Name NVARCHAR(50) NOT NULL,
	Optional BIT NOT NULL,
	Encrypted BIT NOT NULL, 
    CONSTRAINT [FK_TaskTemplateParameters_TaskTemplateVersion] FOREIGN KEY (TaskTemplateVersionId) REFERENCES TaskTemplateVersions(Id)
)
