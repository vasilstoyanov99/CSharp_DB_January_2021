SELECT *
FROM ( SELECT p.[Name] AS 'Name',
		   p.Seats AS 'Seats', 
		   COUNT(t.PassengerId) AS 'Passengers Count'
	   FROM Planes AS p
			LEFT JOIN Flights AS f ON p.Id = f.PlaneId
			LEFT JOIN Tickets AS t ON f.Id = t.FlightId
	   GROUP BY p.[Name], p.Seats) AS [Result]
ORDER BY Result.[Passengers Count] DESC, Result.[Name] ASC, Result.Seats ASC