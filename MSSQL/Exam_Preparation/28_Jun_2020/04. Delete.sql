UPDATE Journeys
SET DestinationSpaceportId = NULL
WHERE Id BETWEEN 1 AND 3

UPDATE Journeys
SET SpaceshipId = NULL
WHERE Id BETWEEN 1 AND 3

UPDATE TravelCards
SET JourneyId = NULL
WHERE JourneyId BETWEEN 1 AND 3

DELETE Journeys 
WHERE Id BETWEEN 1 AND 3