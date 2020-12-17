CREATE PROCEDURE [dbo].[Proc_GetInbox]
	@id UNIQUEIDENTIFIER
AS
	SELECT Id, max(SentDate) as Lastmessage, count(*) as MessageCount,
		(SELECT TOP 1 [Name] FROM dbo.RuggerName n WHERE N.Id = sub.id) AS Name
	FROM (SELECT [FROM] as id, SentDate
		FROM Messages
		WHERE [to] = @id
			AND [FROM] <> @id
		UNION ALL
		SELECT [TO] as id, sentdate
		FROM Messages
		WHERE [from] =  @id
			AND [TO] <> @id
		) AS sub
	GROUP BY Id
	ORDER BY MAX(SentDate) DESC

RETURN 0
