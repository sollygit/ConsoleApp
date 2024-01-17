USE Northwind;
GO
/* 
	Database mail configuration wizard example
	------------------------------------------
	Account name: Gmail
	Description:  Gmail Account
	
	Outgoing mail server
	----------------------------------
	email:        post2person@gmail.com
	Display name: Solly
	Reply email:  Any Reply email
	Server name:  smtp.gmail.com
	Port number:  25
	Reqired SSL:  False
*/

-- A simple update script to check the Trigger
UPDATE Customers SET [ContactName] = 'Maria Ayaka' WHERE CustomerID = 'ALFKI'

-- Drop Trigger
IF OBJECT_ID ('Northwind.dbo.EmailTrigger', 'TR') IS NOT NULL
   DROP TRIGGER dbo.EmailTrigger;
GO

-- Create a Trigger
CREATE TRIGGER EmailTrigger
ON Northwind.dbo.Customers
AFTER INSERT, UPDATE 
AS 
	EXEC msdb.dbo.sp_send_dbmail
	@profile_name = 'Administrator',
	@recipients = 'post2solly@gmail.com',
	@body = 'This email was created by an EmailTrigger on Northwind Customers table.',
	@subject = 'Northwind Trigger Demo';
GO

-- Check if a Trigger is defined
IF OBJECT_ID ('Northwind.dbo.EmailTrigger', 'TR') IS NOT NULL
	PRINT 'EMAIL Trigger is ON'
ELSE
	PRINT 'NO Trigger was found'