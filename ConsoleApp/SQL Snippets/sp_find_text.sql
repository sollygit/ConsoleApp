CREATE PROCEDURE [dbo].[sp_FindText]  
	@QueryText varchar(50)  
AS  
BEGIN  
	SET NOCOUNT ON;  
  
	SELECT DISTINCT name, type   
	FROM sysobjects so INNER JOIN syscomments sc ON so.id = sc.id  
	WHERE TEXT LIKE '%' + @QueryText + '%'  
	ORDER BY name  
END