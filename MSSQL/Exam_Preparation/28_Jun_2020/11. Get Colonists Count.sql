CREATE FUNCTION dbo.udf_GetColonistsCount(@PlanetName VARCHAR (30)) 
RETURNS INT 
AS
	BEGIN 
		RETURN (SELECT COUNT(trc.ColonistId)
				FROM Planets AS p
					JOIN Spaceports AS sp ON p.Id = sp.PlanetId
					JOIN Journeys AS j ON sp.Id = j.DestinationSpaceportId
					JOIN TravelCards AS trc ON j.Id = trc.JourneyId
				WHERE p.[Name] = @PlanetName)
	END