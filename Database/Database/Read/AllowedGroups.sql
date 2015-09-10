CREATE VIEW [rd].[AllowedGroups]
	AS	SELECT	[dbo].AllowedGroups.[Id], 
				[EnviromentId], 
				ApplicationId,
				[GroupId] ,
				AspNetRoles.Name
		FROM [dbo].AllowedGroups INNER JOIN AspNetRoles
									ON AllowedGroups.GroupId = AspNetRoles.Id
