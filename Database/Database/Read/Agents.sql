CREATE VIEW [rd].[Agents]
	AS SELECT [Id], [Name], [Address], [EnviromentId], LastStatusCheck, LastCheck
	   FROM dbo.Agents