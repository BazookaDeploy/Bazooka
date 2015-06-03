CREATE VIEW [rd].[AllowedUsers]
	AS	SELECT	[Id], 
				[UserID], 
				[EnviromentId],
				AspNetUsers.UserName 
		FROM [Dbo].AllowedUsers INNER JOIN AspNetUsers 
									ON UserID = AspNetUsers.Id
