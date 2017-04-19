CREATE TABLE [dbo].[TaskTemplateParameters]
(
	[Id] INT NOT NULL PRIMARY KEY,
	TaskTemplateId INT NOT NULL,
	Name NVARCHAR(50) NOT NULL,
	Optional BIT NOT NULL,
	Encrypted BIT NOT NULL
)
