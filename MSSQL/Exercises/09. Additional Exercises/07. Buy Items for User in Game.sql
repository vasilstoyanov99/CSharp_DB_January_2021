INSERT INTO UserGameItems (ItemId, UserGameId) VALUES
(51, 235),
(71, 235),
(157, 235),
(184, 235),
(197, 235),
(223, 235)

UPDATE UsersGames
SET Cash -= 756.00
WHERE Id = 235

UPDATE UsersGames
SET Cash -= 90.00
WHERE Id = 235

UPDATE UsersGames
SET Cash -= 412.00
WHERE Id = 235

UPDATE UsersGames
SET Cash -= 726.00
WHERE Id = 235

UPDATE UsersGames
SET Cash -= 772.00
WHERE Id = 235

UPDATE UsersGames
SET Cash -= 201.00
WHERE Id = 235

SELECT u.Username, g.[Name], ug.Cash, i.[Name]
FROM UsersGames AS ug
     JOIN Games AS g ON ug.GameId = g.Id
	JOIN Users AS u ON ug.UserId = u.Id
	JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
	JOIN Items AS i ON ugi.ItemId = i.Id
WHERE GameId = 221
ORDER BY i.[Name]