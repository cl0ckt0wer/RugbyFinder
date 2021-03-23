CREATE PROCEDURE [dbo].[Proc_GetInbox] @KEY VARCHAR(64)
AS
DECLARE @id UNIQUEIDENTIFIER;

SELECT @ID = Id
FROM KeyGuid
WHERE [Key] = @KEY;

SELECT Id
	,max(SentDate) AS Lastmessage
	,count(*) AS MessageCount
	,(
		SELECT TOP 1 [Name]
		FROM dbo.RuggerName n
		WHERE N.Id = sub.id
		) AS Name
FROM (
	SELECT [FROM] AS id
		,SentDate
	FROM Messages
	WHERE [To] = @id
		AND [From] <> @id
	
	UNION ALL
	
	SELECT [To] AS id
		,sentdate
	FROM Messages
	WHERE [From] = @id
		AND [To] <> @id
	) AS sub
GROUP BY Id
ORDER BY MAX(SentDate) DESC

RETURN 0
