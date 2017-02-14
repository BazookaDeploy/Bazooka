CREATE TABLE [dbo].[Applications]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] nvarchar(256) NOT NULL, 
    [ApplicationGroupId] INT NULL, 
    [Deleted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Applications_ToApplicationGroups] FOREIGN KEY (ApplicationGroupId) REFERENCES [ApplicationGroups]([Id])
)
