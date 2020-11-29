CREATE FUNCTION [dbo].[sfn_GetClosestCityId]
(
	@geo Geography
)
RETURNS INT
AS
BEGIN
	RETURN (
	SELECT TOP(1) id
	FROM Cities
	WHERE Coordinates.STDistance(@geo) IS NOT NULL  
	ORDER BY Coordinates.STDistance(@geo)
	);  
END
