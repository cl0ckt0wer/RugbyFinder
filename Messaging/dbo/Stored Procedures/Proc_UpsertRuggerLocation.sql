-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Proc_UpsertRuggerLocation
	-- Add the parameters for the stored procedure here
	@geo geography,
	@ui uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	    MERGE dbo.RuggerLocation AS target  
    USING (SELECT @ui, @geo, sysdatetime()) AS source (Id, geo, dtime)  
    ON (target.Id = source.ID)  
    WHEN MATCHED THEN
        UPDATE SET coordinate = source.geo, refreshdate = source.dtime
    WHEN NOT MATCHED THEN  
        INSERT (Id,coordinate, refreshdate)  
        VALUES (source.Id, source.geo, source.dtime);

END