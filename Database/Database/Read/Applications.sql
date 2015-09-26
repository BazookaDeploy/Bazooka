CREATE VIEW [rd].[Applications]
	AS SELECT [Applications].Id, [Applications].[Name], [Applications].[ApplicationGroupId], ApplicationGroups.Name as GroupName FROM [dbo].Applications LEFT JOIN dbo.ApplicationGroups on ApplicationGroupId = ApplicationGroups.Id
