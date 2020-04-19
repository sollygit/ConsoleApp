SELECT COUNT(*) AS [Number of Employees] FROM Employees;
SELECT TOP (10) ProductName FROM Products WHERE ProductName LIKE 'A%';
SELECT TOP (10) ProductName FROM Products WHERE ProductName > 'A' and ProductName < 'C';
SELECT TOP (10) ProductName FROM Products ORDER BY ProductName;

SELECT DISTINCT
	C.CustomerID,
	C.CompanyName
FROM
	dbo.Customers AS C INNER JOIN 
	dbo.Orders AS O ON O.CustomerID = C.CustomerID
WHERE 
	O.OrderDate >= '19980101' AND O.OrderDate < '19990101'
ORDER BY 
	C.CompanyName

SELECT
	s.ShipperID, s.CompanyName, o.OrderID, o.ShippedDate,
	e.EmployeeID, e.LastName, o.CustomerID, c.CompanyName
FROM
	Shippers s
	join Orders o on o.ShipVia = s.ShipperID
	join Employees e on e.EmployeeID = o.EmployeeID
	join Customers c on c.CustomerID = o.CustomerID
WHERE 
	-- o.ShippedDate is not null
	CAST(o.ShippedDate AS DATE) > '19980501'
ORDER BY
	ShipperID, ShippedDate desc
