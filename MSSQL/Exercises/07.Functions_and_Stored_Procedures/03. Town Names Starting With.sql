CREATE PROC usp_GetTownsStartingWith 
		(@Parameter NVARCHAR(4))
AS
BEGIN
	SELECT [Name] AS 'Town'
	FROM Towns
	WHERE [Name] LIKE @Parameter + '%'
END
GO

EXEC usp_GetTownsStartingWith "b"