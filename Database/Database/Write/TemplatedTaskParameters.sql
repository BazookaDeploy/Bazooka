CREATE TABLE [dbo].[TemplatedTaskParameters]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	TemplatedTaskId int not null,
	TaskTemplateParameterId int not null,
	Value nvarchar(250) not null, 
    CONSTRAINT [FK_TemplatedTaskParameters_ToTemplatedTasks] FOREIGN KEY (TemplatedTaskId) REFERENCES TemplatedTask(Id),
    CONSTRAINT [FK_TemplatedTaskParameters_ToTemplatedTasksParaameters] FOREIGN KEY (TaskTemplateParameterId) REFERENCES TaskTemplateParameters(Id)
)
