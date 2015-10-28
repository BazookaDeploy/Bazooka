CREATE TABLE [dbo].[MailTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[Text] NVARCHAR(MAX) NOT NULL,
	[Recipients] NVARCHAR(256) NOT NULL,
	[EnviromentId] INT NOT NULL, 
    [Sender] NVARCHAR(256) NULL, 
    [ApplicationId] INT NOT NULL, 
    CONSTRAINT [FK_MailTasks_ToEnviroments] FOREIGN KEY ([EnviromentId]) REFERENCES [Enviroments]([Id]), 
    CONSTRAINT [FK_MailTasks_ToApplications] FOREIGN KEY (ApplicationId) REFERENCES Applications([Id])


)
