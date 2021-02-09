SELECT p.FirstName + ' ' + p.LastName AS 'Full Name',
	   pl.[Name] AS 'Plane Name',
	   f.Origin + ' - ' + f.Destination AS 'Trip',
	   lgt.[Type] AS 'Luggage Type'
FROM Passengers AS p
	JOIN Tickets AS t ON p.Id = t.PassengerId
	JOIN Flights AS f ON f.Id = t.FlightId
	JOIN Planes AS pl ON pl.Id = f.PlaneId
	JOIN Luggages AS lgs ON lgs.Id = t.LuggageId
	JOIN LuggageTypes AS lgt ON lgt.Id = lgs.LuggageTypeId 
	 
ORDER BY [Full Name] ASC, 
	     [Plane Name] ASC, 
		 Origin ASC, 
		 Destination ASC,
		 [Luggage Type] ASC