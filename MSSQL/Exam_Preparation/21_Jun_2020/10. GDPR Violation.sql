SELECT t.Id, 
	   a.FirstName + ' ' +
		ISNULL(a.MiddleName + ' ', '')  +
		 a.LastName AS 'Full Name',
		 (SELECT TOP(1) [Name] FROM Cities AS c WHERE c.Id = a.CityId) 'From',
		 (SELECT TOP(1) [Name] FROM Cities AS c WHERE c.Id = h.CityId) AS 'To',
		 IIF(t.CancelDate IS NOT NULL, 
				'Canceled', 
				CONCAT(DATEDIFF(day, t.ArrivalDate, t.ReturnDate), ' days')) AS 'Duration'
FROM Accounts AS a
	JOIN AccountsTrips AS atr ON a.Id = atr.AccountId
	JOIN Trips AS t ON atr.TripId = t.Id
	JOIN Rooms AS r ON t.RoomId = r.Id
	JOIN Hotels AS h ON r.HotelId = h.Id
	JOIN Cities AS c ON h.CityId = c.Id
ORDER BY [Full Name] ASC, t.Id ASC
