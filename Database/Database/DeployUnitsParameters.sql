CREATE TABLE [dbo].[DeployUnitsParameters]
(
	[DeployUnitId] INT NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Value] NVARCHAR(256) NOT NULL, 
    CONSTRAINT [FK_DeployUnitsParameters_DeployUnits] FOREIGN KEY ([DeployUnitId]) REFERENCES [DeployUnits]([Id])
)
