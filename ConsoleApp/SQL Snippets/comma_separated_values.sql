SELECT Id
FROM   dbo.Customer
WHERE  City = 'Auckland'

DECLARE @IDs nvarchar(max)

SELECT @IDs = COALESCE(@IDs + ',', '') + CAST(ID as nvarchar(300))
FROM   dbo.Customer
WHERE  City = 'Auckland'

SELECT @IDs AS [CustomerIDs]
