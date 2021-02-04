CREATE FUNCTION ufn_CashInUsersGames (@gameName VARCHAR(MAX))
RETURNS TABLE AS
		RETURN 
			(
				SELECT SUM(Cash) AS 'SumCash'
				FROM (SELECT ug.Cash,
					ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS 'RowNumber'
					FROM Games AS g
					JOIN UsersGames AS ug ON g.Id = ug.GameId
					WHERE g.[Name] = @gameName) AS [Result]
				WHERE RowNumber % 2 != 0
			)

GO 

SELECT * 
FROM dbo.ufn_CashInUsersGames('Love in a mist')