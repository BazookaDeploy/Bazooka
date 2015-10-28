CREATE VIEW [rd].[AllowedUsers]
	AS	SELECT	[Dbo].AllowedUsers.[Id], 
				[UserID], 
				[EnviromentId],
				ApplicationId,
				AspNetUsers.UserName 
		FROM [Dbo].AllowedUsers INNER JOIN AspNetUsers 
									ON UserID = AspNetUsers.Id
