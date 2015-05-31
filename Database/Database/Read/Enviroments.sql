CREATE VIEW [rd].[Enviroments]
	AS SELECT Enviroments.Id, 
			  Enviroments.ApplicationId, 
			  Enviroments.Configuration, 
			  Enviroments.Description, 
			  Applications.Name,
			  Enviroments.OwnerId,
			  AspNetUsers.UserName
		FROM [dbo].Enviroments 
			 INNER JOIN [dbo].[Applications] ON Enviroments.ApplicationId = Applications.Id
			 INNER JOIN [dbo].[AspNetUsers] ON Enviroments.OwnerId = AspNetUsers.Id
