CREATE VIEW [rd].[Users] AS 
	SELECT	[Id], 
			[Email], 
			[UserName] ,
			Administrator,
			ConfigurationManager
	FROM [dbo].AspNetUsers
