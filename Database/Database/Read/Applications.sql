﻿CREATE VIEW [rd].[Applications]
	AS SELECT [Applications].Id, 
	          [Applications].[Name], 
			  [Applications].[ApplicationGroupId], 
			  [Applications].Secret, 
			  ApplicationGroups.Name as GroupName 
			  FROM [dbo].Applications LEFT JOIN dbo.ApplicationGroups on ApplicationGroupId = ApplicationGroups.Id
	where Applications.Deleted = 0
