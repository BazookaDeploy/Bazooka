
CREATE VIEW [rd].[Deployers]
	AS
	SELECT T.EnviromentId, T.ApplicationId, T.UserId
	FROM (
		SELECT	Enviroments.Id as EnviromentId, Applications.Id as ApplicationId, AllowedUsers.UserID as UserId
			FROM [dbo].Enviroments,  Applications, [Dbo].AllowedUsers 
			WHERE Applications.Id = AllowedUsers.ApplicationId AND Enviroments.Id = AllowedUsers.EnviromentId

			UNION

		SELECT	Enviroments.Id as EnviromentId, Applications.Id as ApplicationId, AspNetUserRoles.UserId
			FROM [dbo].Enviroments, Applications, [Dbo].AllowedGroups , dbo.AspNetUserRoles
			WHERE Applications.Id = AllowedGroups.ApplicationId AND Enviroments.Id = AllowedGroups.EnviromentId AND AllowedGroups.GroupId = AspNetUserRoles.RoleId
	) AS T