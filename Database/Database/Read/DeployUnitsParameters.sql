CREATE VIEW [rd].[DeployUnitsParameters]
	AS SELECT Id as ParameterId, Name , Value, DeployUnitId, Encrypted
	FROM [dbo].[DeployUnitsParameters]
