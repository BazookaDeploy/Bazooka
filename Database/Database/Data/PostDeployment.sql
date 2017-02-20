/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
MERGE INTO AspNetUsers AS Target 
USING (VALUES 
           ('00000000-0000-0000-0000-000000000000',
		   'admin@bazooka.com'
           ,1
           ,''
           ,''
           ,NULL
           ,0
           ,0
           ,NULL
           ,0
           ,0
           ,'system'
           ,1)
) 
AS Source ([Id],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName],[Administrator])
ON Target.[Id] = Source.[Id] 
WHEN MATCHED THEN 
UPDATE SET [UserName] = Source.[UserName] 

WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName],[Administrator])
VALUES ([Id],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEndDateUtc],[LockoutEnabled],[AccessFailedCount],[UserName],[Administrator]); 