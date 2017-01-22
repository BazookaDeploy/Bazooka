CREATE TABLE [dbo].[AllowedGroups]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EnviromentId] INT NOT NULL, 
    [GroupId] NVARCHAR(128) NOT NULL,
	 
    [ApplicationId] INT NOT NULL, 
    CONSTRAINT [FK_AllowedGroups_Enviroments] 
		FOREIGN KEY ([EnviromentId]) 
		REFERENCES [Enviroments]([Id]), 

    CONSTRAINT [FK_AllowedGroups_Groups] 
		FOREIGN KEY ([GroupId]) 
		REFERENCES [AspNetRoles]([Id]),

	CONSTRAINT [FK_AllowedGroups_Applications] 
		FOREIGN KEY ([ApplicationId]) 
		REFERENCES [Applications]([Id]), 
    CONSTRAINT [CK_AllowedGroups_Unique] UNIQUE(EnviromentId,GroupId,ApplicationId) 
)
