CREATE TABLE [dbo].[AllowedUsers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserID] NVARCHAR(128) NOT NULL, 
    [EnviromentId] INT NOT NULL, 

    [ApplicationId] INT NOT NULL, 
    CONSTRAINT [FK_AllowedUsers_Users] 
		FOREIGN KEY ([UserId]) 
		REFERENCES [AspNetUsers]([Id]), 

    CONSTRAINT [FK_AllowedUsers_Enviroment] 
		FOREIGN KEY ([EnviromentId]) 
		REFERENCES [Enviroments]([Id]) ,

    CONSTRAINT [C_AllowedUsers_Unique]
	    UNIQUE (UserID,EnviromentId, ApplicationId),

	CONSTRAINT [FK_AllowedUsers_Application] 
		FOREIGN KEY ([ApplicationId]) 
		REFERENCES [Applications]([Id]) 
)
