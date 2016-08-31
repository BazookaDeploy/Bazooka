CREATE VIEW rd.[ApplicationAdministrators]
	AS SELECT ApplicationAdministrators.Id, 
			  ApplicationAdministrators.UserID, 
			  ApplicationAdministrators.ApplicationId
			  Applications.Name, 
			  AspNetUsers.UserName 
		FROM dbo.ApplicationAdministrators
				join dbo.AspNetUsers on ApplicationAdministrators.UserID = AspNetUsers.Id
				join dbo.Applications on ApplicationAdministrators.ApplicationId = dbo.Applications.Id
