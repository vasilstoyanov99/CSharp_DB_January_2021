SELECT a.Id, a.Email, c.[Name], COUNT(c.Id) AS 'Trips'
FROM Accounts AS a
	JOIN AccountsTrips AS atr ON a.Id = atr.AccountId
	JOIN Trips AS t ON atr.TripId = t.Id
	JOIN Rooms AS r ON t.RoomId = r.Id
	JOIN Hotels AS h ON r.HotelId = h.Id
	JOIN Cities AS c ON h.CityId = c.Id
WHERE t.CancelDate IS NULL AND c.Id = a.CityId
GROUP BY a.Id, a.Email, c.[Name]
ORDER BY Trips DESC, a.Id