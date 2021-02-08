SELECT *
FROM (SELECT tc.JobDuringJourney, 
		   c.FirstName + ' ' + c.LastName AS 'FullName',
		   RANK() OVER(PARTITION BY tc.JobDuringJourney ORDER BY c.BirthDate ASC)
			AS 'JobRank'
	 FROM Colonists AS c
		JOIN TravelCards AS tc ON c.Id = tc.ColonistId
) AS Result
WHERE Result.JobRank = 2