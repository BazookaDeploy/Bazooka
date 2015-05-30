CREATE VIEW [rd].[Deployments]
	AS SELECT [dbo].Deployments.*, 
			  Applications.Name, 
			  Enviroments.Configuration ,
			  AspNetUsers.UserName
			  FROM [dbo].[Deployments] 
					INNER JOIN [dbo].[Enviroments] ON Deployments.EnviromentId = Enviroments.Id
					INNER JOIN  [dbo].[Applications] ON Enviroments.ApplicationId = Applications.Id
					INNER JOIN [dbo].[AspNetUsers] ON Deployments.UserId = AspNetUsers.Id
