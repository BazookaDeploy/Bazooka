CREATE VIEW [rd].[DeployUnits]
	AS SELECT [dbo].DeployUnits.*, Applications.Name AS ApplicationName, Enviroments.Description as EnviromentName
	FROM [dbo].DeployUnits JOIN  [dbo].Enviroments ON DeployUnits.EnviromentId = Enviroments.Id
						   JOIN  [dbo].Applications ON Enviroments.ApplicationId = Applications.Id
