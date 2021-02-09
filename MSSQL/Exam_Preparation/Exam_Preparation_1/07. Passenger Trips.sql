SELECT p.FirstName + ' ' + p.LastName AS 'Full Name',
	   f.Origin,
	   f.Destination
FROM Passengers AS p
	JOIN Tickets AS t ON p.Id = t.PassengerId
	JOIN Flights AS f ON f.Id = t.FlightId
ORDER BY [Full Name] ASC, Origin ASC, Destination ASC