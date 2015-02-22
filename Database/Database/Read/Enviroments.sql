CREATE VIEW [rd].[Enviroments]
	AS SELECT Enviroments.Id, Enviroments.ApplicationId, Enviroments.Configuration, Enviroments.Description, Applications.Name
		FROM [dbo].Enviroments 
			 INNER JOIN [dbo].[Applications] 
			 ON Enviroments.ApplicationId = Applications.Id
