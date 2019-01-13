CREATE TABLE [dbo].[MaintenanceTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	AgentId int not null,
	TemplatedTaskId int not null, 
	StartDate DATETIME not null, 
	Status int not null,
	UserId NVARCHAR(250) not null
)
