CREATE TABLE [dbo].[ApplicationsInGroups]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ApplicationGroupId] INT NOT NULL, 
    [ApplicationId] INT NOT NULL, 
    CONSTRAINT [FK_ApplicationsInGroups_ToApplicationGroups] FOREIGN KEY ([ApplicationGroupId]) REFERENCES [ApplicationGroups](Id), 
    CONSTRAINT [FK_ApplicationsInGroups_ToApplication] FOREIGN KEY ([ApplicationId]) REFERENCES [Applications](Id)
)
