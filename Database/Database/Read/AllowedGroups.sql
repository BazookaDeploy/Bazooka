CREATE VIEW [rd].[AllowedGroups]
	AS	SELECT	[Id], 
				[EnviromentId], 
				[GroupId] ,
				AspNetRoles.Name
		FROM [dbo].AllowedGroups INNER JOIN AspNetRoles
									ON AllowedGroups.GroupId = AspNetRoles.Id
