UPDATE Flights
SET PlaneId = NULL
WHERE [Destination] = 'Ayn Halagim'

UPDATE Tickets
SET FlightId = NULL
WHERE FlightId = 30

DELETE Flights
WHERE Id = 30