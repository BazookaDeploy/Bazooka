CREATE VIEW [rd].[DeployTasksParameters]
	AS SELECT Id as ParameterId, Name , Value, DeployTaskId, Encrypted
	FROM [dbo].[DeployTasksParameters]
