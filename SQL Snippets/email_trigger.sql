USE QuickApp
GO
/* 
	Database mail configuration wizard example
	------------------------------------------
	Account name: Gmail
	Description:  Gmail Account
	
	Outgoing mail server
	----------------------------------
	email:        mail4solly@gmail.com
	Display name: Solly
	Reply email:  Any Reply email
	Server name:  smtp.gmail.com
	Port number:  25
	Reqired SSL:  False
*/

-- A simple update script to check the Trigger
UPDATE Customer SET [Name] = 'Maria Ayaka' WHERE Id = 1

-- Drop Trigger
IF OBJECT_ID ('QuickApp.dbo.EmailTrigger', 'TR') IS NOT NULL
   DROP TRIGGER dbo.EmailTrigger;
GO

-- Create a Trigger
CREATE TRIGGER EmailTrigger
ON QuickApp.dbo.Customer
AFTER INSERT, UPDATE 
AS 
	EXEC msdb.dbo.sp_send_dbmail
	@profile_name = 'Administrator',
	@recipients = 'mail4solly@gmail.com',
	@body = 'This email was created by an EmailTrigger on QuickApp Customer table.',
	@subject = 'QuickApp Trigger Demo';
GO

-- Check if a Trigger is defined
IF OBJECT_ID ('QuickApp.dbo.EmailTrigger', 'TR') IS NOT NULL
	PRINT 'EMAIL Trigger is ON'
ELSE
	PRINT 'NO Trigger was found'

