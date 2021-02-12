SELECT u.Username, 
	   g.[Name] AS 'Game', 
	   COUNT(i.ItemTypeId) AS 'Items Count',
	   SUM(i.Price) AS 'Items Price'
FROM Games AS g
	JOIN UsersGames AS ug ON ug.GameId = g.Id
	JOIN Users AS u ON ug.UserId = u.Id
	JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
	JOIN Items AS i ON ugi.ItemId = i.Id
GROUP BY u.Username, g.[Name]
HAVING COUNT(i.ItemTypeId) >= 10
ORDER BY 'Items Count' DESC, 'Items Price' DESC, u.Username ASC