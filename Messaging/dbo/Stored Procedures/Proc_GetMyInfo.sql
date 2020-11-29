CREATE PROCEDURE [dbo].[Proc_GetMyInfo]
	@uid uniqueidentifier,
	@Name NVARCHAR(50) OUT,
	@BIO NVARCHAR(MAX) OUT
AS
	SET NOCOUNT ON;

	SELECT TOP (1) @NAME = Name, @BIO = Bio
	FROM DBO.RuggerName
	WHERE Id = @uid
RETURN 1
