CREATE VIEW [rd].[Users] AS 
	SELECT	[Id], 
			[Email], 
			[UserName] ,
			Administrator
	FROM [dbo].AspNetUsers
