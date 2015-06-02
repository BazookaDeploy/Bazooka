CREATE VIEW [rd].[UsersInGroups]
	AS SELECT	[Dbo].[AspNetUsers].[Email], 
				[Dbo].[AspNetUsers].[UserName], 
				[dbo].[AspNetUserRoles].[UserId], 
				[dbo].[AspNetUserRoles].[RoleId], 
				[dbo].[AspNetRoles].[Name] as RoleName
	FROM Dbo.AspNetUsers	INNER JOIN dbo.AspNetUserRoles 
								ON AspNetUsers.Id = AspNetUserRoles.UserId
							INNER JOIN dbo.AspNetRoles 
								ON dbo.AspNetUserRoles.RoleId = AspNetRoles.Id
