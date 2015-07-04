CREATE VIEW [rd].[Deployments]
	AS SELECT [dbo].[Deployments].[Id], 
			  [dbo].[Deployments].[EnviromentId], 
			  [dbo].[Deployments].[StartDate], 
			  [dbo].[Deployments].[EndDate], 
			  [dbo].[Deployments].[Status], 
			  [dbo].[Deployments].[Version], 
			  [dbo].[Deployments].[UserId], 
			  Applications.Name, 
			  Enviroments.Configuration ,
			  AspNetUsers.UserName
			  FROM [dbo].[Deployments] 
					INNER JOIN [dbo].[Enviroments] ON Deployments.EnviromentId = Enviroments.Id
					INNER JOIN  [dbo].[Applications] ON Enviroments.ApplicationId = Applications.Id
					INNER JOIN [dbo].[AspNetUsers] ON Deployments.UserId = AspNetUsers.Id
