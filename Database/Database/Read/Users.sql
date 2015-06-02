CREATE VIEW [rd].[Users] AS 
	SELECT	[Id], 
			[Email], 
			[UserName] 
	FROM [dbo].AspNetUsers
