SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [Ints_Split] (
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
	WHERE Item IS NOT NULL
);
/*
C# Code:
	command.Parameters.AddWithValue("@TechnologyList", String.Join(",", customer.TechnologyList.Select(t => t.TechnologyId)));
SP Use:
	@TechnologyList varchar(max) (Represents a comma seperated values of ints: 1,2,3,4)
	INSERT INTO [CustomerTechnology] (CustomerID, TechnologyID)
	SELECT @CustomerID, TechnologyID = Item FROM Ints_Split(@TechnologyList, ',')
*/
GO