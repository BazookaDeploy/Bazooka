﻿CREATE TABLE dbo.[TaskTemplate]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	Name NVARcHAR(100) NOT NULL,
	Description NVARCHAR(2000), 
    [Deleted] BIT NOT NULL DEFAULT 0 
)
