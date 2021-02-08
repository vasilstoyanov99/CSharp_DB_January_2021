SELECT ssh.[Name], ssh.Manufacturer
FROM Spaceships AS ssh
	JOIN Journeys AS j ON ssh.Id = j.SpaceshipId
	JOIN TravelCards AS trc ON j.Id = trc.JourneyId
	JOIN Colonists AS c ON c.Id = trc.ColonistId
WHERE YEAR(c.BirthDate) > 1989 AND trc.JobDuringJourney = 'Pilot'
GROUP BY ssh.[Name], ssh.Manufacturer
ORDER BY ssh.[Name]