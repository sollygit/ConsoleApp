
SELECT * FROM [Order Details] WHERE OrderID = 10248
BEGIN TRANSACTION
	UPDATE [Order Details]
	SET UnitPrice = UnitPrice + (UnitPrice * .1) -- Updates UnitPrice by 10%
	WHERE OrderID = 10248
SAVE TRANSACTION Upd_UnitPrice10Perc;
ROLLBACK TRANSACTION

-- Some Commands to remember by:
SELECT @@SPID --Session Number.
EXEC sp_lock -- find out about the locks
dbcc useroptions --Option Settings.
EXEC sp_who2 52 --User's session state/ More info
EXEC sp_who 52 --User's session state/In General

/*
Note: This NOLOCK is not good to use with Update statements
because it may not be dependable or good data.
Select statements are OK to use when it is needed.
*/

-- SELECT TOP (10) * FROM Customers
SELECT Country, COUNT(CustomerID) CustomerCount
FROM Customers WITH(NOLOCK)
GROUP BY Country
ORDER BY CustomerCount DESC