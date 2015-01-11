CREATE VIEW [rd].[DeployUnitsParameters]
	AS SELECT Id as ParameterId, Name , Value, DeployUnitId
	FROM [dbo].[DeployUnitsParameters]
