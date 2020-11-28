-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_GetClosestCity] 
	-- Add the parameters for the stored procedure here
	@geo geography,
	@ui uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	exec Proc_UpsertRuggerLocation @geo, @ui

	SELECT TOP(1) Country, admin_name, city, Coordinates.STDistance(@geo) as StDistance
	FROM Cities
	WHERE Coordinates.STDistance(@geo) IS NOT NULL  
	ORDER BY Coordinates.STDistance(@geo);  

END