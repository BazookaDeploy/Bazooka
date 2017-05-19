CREATE TABLE [dbo].[TemplatedTaskParameters]
(
	[Id] INT NOT NULL PRIMARY KEY,
	TemplatedTaskId int not null,
	Name nvarchar(250) not null,
	Value nvarchar(250) not null, 
    CONSTRAINT [FK_TemplatedTaskParameters_ToTemplatedTasks] FOREIGN KEY (TemplatedTaskId) REFERENCES TemplatedTask(Id)
)
