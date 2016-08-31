CREATE TABLE [dbo].ApplicationAdministrators
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserID] NVARCHAR(128) NOT NULL, 
    [ApplicationId] INT NOT NULL, 
    CONSTRAINT [FK_ApplicationAdministrators_Users] 
		FOREIGN KEY ([UserId]) 
		REFERENCES [AspNetUsers]([Id]), 

	CONSTRAINT [FK_ApplicationAdministrators_Application] 
		FOREIGN KEY ([ApplicationId]) 
		REFERENCES [Applications]([Id]) 
)
