SELECT COUNT(*) AS [Number of Employees] FROM Employees;
SELECT TOP (10) ProductName FROM Products WHERE ProductName LIKE 'A%';
SELECT TOP (10) ProductName FROM Products WHERE ProductName > 'A' and ProductName < 'C';
SELECT TOP (10) ProductName FROM Products ORDER BY ProductName;
GO

SELECT * FROM Customers C INNER JOIN Orders O ON C.CustomerID = O.CustomerID;
SELECT * FROM Customers C LEFT JOIN Orders O ON C.CustomerID = O.CustomerID;
SELECT * FROM Customers C LEFT JOIN Orders O ON C.CustomerID = O.CustomerID WHERE O.OrderID IS NULL;
SELECT * FROM Customers C RIGHT JOIN Orders O ON C.CustomerID = O.CustomerID;
SELECT * FROM Orders O WHERE OrderID IS NULL;
GO

-- Self Join: SQL statement that matches customers that are from the same city:
SELECT C1.CustomerID AS CustomerID1, C2.CustomerID AS CustomerID2, C1.City
FROM Customers C1, Customers C2
WHERE C1.CustomerID <> C2.CustomerID AND C1.City = C2.City
ORDER BY C1.City;
GO

SELECT * FROM Customers C FULL JOIN Orders O ON C.CustomerID = O.CustomerID;
SELECT * FROM Customers C CROSS JOIN Orders;
GO

SELECT DISTINCT
	C.CustomerID, C.CompanyName
FROM
	dbo.Customers C 
		INNER JOIN dbo.Orders O ON O.CustomerID = C.CustomerID
WHERE 
	O.OrderDate >= '19980101' AND O.OrderDate < '19990101'
ORDER BY 
	C.CompanyName
GO

SELECT
	s.ShipperID, s.CompanyName, o.OrderID, o.ShippedDate,
	e.EmployeeID, e.LastName, o.CustomerID, c.CompanyName
FROM
	Shippers s
		INNER JOIN Orders o on o.ShipVia = s.ShipperID
		INNER JOIN Employees e on e.EmployeeID = o.EmployeeID
		INNER JOIN Customers c on c.CustomerID = o.CustomerID
WHERE 
	-- o.ShippedDate is not null
	CAST(o.ShippedDate AS DATE) > '19980501'
ORDER BY
	ShipperID, ShippedDate desc
GO

/* Write a query that selects average, highest and lowest sale order for all customer. */
WITH Sales_CTE (SalesPersonID, NumberOfOrders)  
AS (  
    SELECT CustomerID, COUNT(*)  
    FROM Orders
    WHERE CustomerID IS NOT NULL  
    GROUP BY CustomerID)  
SELECT 
	AVG(NumberOfOrders) AS "Average sales per customer",
	MAX(NumberOfOrders) AS "Highest sale of customer",
	MIN(NumberOfOrders) AS "Lowest sale of customer"  
FROM 
	Sales_CTE;
GO

/* Write a query that selects CustomerID, number of orders and average Freight weight for each customer who has more than 3 orders. */
SELECT
	CustomerID,
	COUNT(*) as [Number of Orders],
	AVG(Freight) as [Average Freight]
FROM Orders 
WHERE 
	CustomerID in (select CustomerID from Orders group by CustomerID having count(*) > 3)
GROUP BY 
	CustomerID;
GO

/* Write a query that select all distinct cities from Customers and Suppliers. */
SELECT DISTINCT [City] FROM (select City from Customers union all select City from Suppliers) u
GO

-- Select into CSV format
DECLARE @CustomerIDs nvarchar(max)
SELECT CustomerID FROM dbo.Customers WHERE City = 'Portland'
SELECT @CustomerIDs = COALESCE(@CustomerIDs + ',', '') + CAST(CustomerID as nvarchar(100)) FROM dbo.Customers WHERE City = 'Portland'
SELECT @CustomerIDs AS [CustomerIDs];
GO

-- Updates UnitPrice by 10% in tran
SELECT * FROM [Order Details] WHERE OrderID = 10248
BEGIN TRANSACTION
	UPDATE [Order Details]
	SET UnitPrice = UnitPrice + (UnitPrice * .1)
	WHERE OrderID = 10248
SAVE TRANSACTION Upd_UnitPrice10Perc;
ROLLBACK TRANSACTION;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [Ints_Split] (
	@List      VARCHAR(MAX),
	@Delimiter VARCHAR(255)
)
RETURNS TABLE
AS
RETURN (
	SELECT Item = CONVERT(INT, Item) FROM
		(SELECT Item = x.i.value('(./text())[1]', 'varchar(max)')
			FROM ( SELECT [XML] = CONVERT(XML, '<i>'
				+ REPLACE(@List, @Delimiter, '</i><i>') + '</i>').query('.')
				) AS a CROSS APPLY [XML].nodes('i') AS x(i) 
		) AS y
	WHERE Item IS NOT NULL);
/*
C# Code:
	command.Parameters.AddWithValue("@TechnologyList", String.Join(",", customer.TechnologyList.Select(t => t.TechnologyId)));
SP Use:
	@TechnologyList varchar(max) (Represents a comma seperated values of ints: 1,2,3,4)
	INSERT INTO [CustomerTechnology] (CustomerID, TechnologyID)
	SELECT @CustomerID, TechnologyID = Item FROM Ints_Split(@TechnologyList, ',')
*/
GO

ALTER PROCEDURE [dbo].[sp_FindText]
	@QueryText varchar(50)  
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT DISTINCT name, type   
	FROM sysobjects so INNER JOIN syscomments sc ON so.id = sc.id  
	WHERE TEXT LIKE '%' + @QueryText + '%'  
	ORDER BY name  
END
GO

ALTER PROCEDURE [dbo].[sp_UpdateCustomer]
	@CustomerID nvarchar(50),
	@ContactName nvarchar(50),
	@City nvarchar(50) = NULL,
	@Result int = NULL OUTPUT
AS        
BEGIN        
	SET NOCOUNT ON;     
	BEGIN TRY
		BEGIN TRAN
			UPDATE dbo.Customers
			SET  
				[ContactName] = @ContactName,
				[City] = @City
			WHERE 
				CustomerID = @CustomerID
			
			SET @Result  = @@ROWCOUNT; -- SUCCESS
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		 SET @Result = 0 --ERROR
		 ROLLBACK TRAN;
	END CATCH
	SELECT @Result AS Result
END

-- Some useful commands to remember:
DBCC CHECKIDENT ('[TABLE_NAME]', RESEED, 0); -- Reseed on table
SELECT @@SPID    -- Session number
EXEC sp_lock     -- Find out about the locks
dbcc useroptions -- Option settings
EXEC sp_who2 52  -- User's session state - more info
EXEC sp_who 52   -- User's session state - general

/*
Note: This NOLOCK is not good to use with Update statements because it may not be dependable or good data.
Select statements are OK to use when it is needed.
*/
-- SELECT TOP (10) * FROM Customers
SELECT Country, COUNT(CustomerID) as [CustomerCount]
FROM Customers WITH(NOLOCK)
GROUP BY Country
ORDER BY CustomerCount DESC;

/*
	The following two tables are used to define users and their respective roles:
	CREATE TABLE users (
	  id INTEGER NOT NULL PRIMARY KEY,
	  userName VARCHAR(50) NOT NULL)

	CREATE TABLE roles(
	  id INTEGER NOT NULL PRIMARY KEY,
	  role VARCHAR(20) NOT NULL)
	  
	The users_roles table should contain the mapping between each user and their roles. 
	Each user can have many roles, and each role can have many users.

	Modify the provided SQLite create table statement so that:

	1. Only users from the users table can exist within users_roles.
	2. Only roles from the roles table can exist within users_roles.
	3. A user can only have a specific role once.

	CREATE TABLE users_roles (
	  userId INTEGER NOT NULL,
	  roleId INTEGER NOT NULL,
	  FOREIGN KEY(userId) REFERENCES users(id),
	  FOREIGN KEY(roleId) REFERENCES roles(id),
	  PRIMARY KEY (userId, roleId)
	)
*/
GO