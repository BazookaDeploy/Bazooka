CREATE TABLE [dbo].[MaintenanceLogEntries]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	MaintenanceTaskId int not null,
	Text nvarchar(mAX) 
)
