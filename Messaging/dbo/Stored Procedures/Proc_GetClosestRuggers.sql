
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_GetClosestRuggers] 
	-- Add the parameters for the stored procedure here
	@geo geography,
	@ui uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	WITH CTE AS (
		SELECT TOP(100) Id, 
		ROW_NUMBER() OVER (ORDER BY Coordinate.STDistance(@geo)) AS LOCATIONORDER
		FROM RuggerLocation l
		WHERE Coordinate.STDistance(@geo) IS NOT NULL  
		ORDER BY Coordinate.STDistance(@geo))
	SELECT N.Name, CTE.LOCATIONORDER, cte.Id as [guid], n.Bio
	FROM CTE
	LEFT JOIN RuggerName N ON N.Id = CTE.Id
	LEFT JOIN RuggerTeam RT ON RT.RuggerId = N.Id
	LEFT JOIN Teams T ON T.Id = RT.TeamId
	LEFT JOIN RuggerPic P ON P.Id = CTE.Id
END