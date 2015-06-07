CREATE VIEW [rd].[Deployers]
	AS 

	SELECT Dep.EnviromentId, 
		   Dep.OwnerId AS UserId, 
		   Dep.ApplicationId, 
		   AspNetUsers.UserName, 
		   Applications.Name AS ApplicationName, 
		   Dep.Configuration
	FROM
	(	SELECT	Id AS EnviromentId, 
				ApplicationId, 
				OwnerId, 
				Configuration 
		FROM [dbo].Enviroments
		
		UNION

		SELECT	Enviroments.Id AS EnviromentId, 
				ApplicationId, 
				OwnerId, 
				Configuration 
		FROM [dbo].Enviroments	INNER JOIN [dbo].AllowedUsers ON 
									Enviroments.Id = AllowedUsers.EnviromentId

		UNION

		SELECT	Enviroments.Id AS EnviromentId, 
				ApplicationId,  
				AspNetUserRoles.RoleId as OwnerId, 
				Configuration 
		FROM [dbo].Enviroments	INNER JOIN [dbo].AllowedGroups ON 
									Enviroments.Id = AllowedGroups.EnviromentId
								INNER JOIN [dbo].AspNetUserRoles ON 
									AllowedGroups.GroupId = AspNetUserRoles.RoleId
	) AS Dep 
	
	INNER JOIN [Dbo].AspNetUsers ON Dep.OwnerId =AspNetUsers.Id
	INNER JOIN [dbo].Applications ON Applications.Id = Dep.ApplicationId

