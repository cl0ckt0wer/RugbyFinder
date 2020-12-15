CREATE PROCEDURE [dbo].[Proc_GetInbox]
	@id UNIQUEIDENTIFIER
AS
	SELECT m.[From] as Id, MAX(M.SentDate) as LastMessage, COUNT(M.SentDate) as MessageCount,
		(SELECT TOP 1 [Name] FROM dbo.RuggerName n WHERE N.Id = M.[From]) AS Name
	FROM dbo.Messages M
	WHERE M.[To] = @id
	GROUP BY M.[From]
	ORDER BY MAX(M.SentDate) DESC

RETURN 0
