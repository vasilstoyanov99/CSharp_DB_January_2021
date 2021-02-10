SELECT a.Id AS 'AccountId', 
	   a.FirstName + ' ' + a.LastName AS 'FullName',
	   MAX(DATEDIFF(day, t.ArrivalDate, t.ReturnDate)) AS 'LongestTrip',
	   MIN(DATEDIFF(day, t.ArrivalDate, t.ReturnDate)) AS 'ShortestTrip'
FROM Accounts AS a
	JOIN AccountsTrips AS atr ON a.Id = atr.AccountId
	LEFT JOIN Trips AS t ON atr.TripId = t.Id
WHERE a.MiddleName IS NULL AND T.CancelDate IS NULL
GROUP BY a.Id, a.FirstName, a.LastName
ORDER BY LongestTrip DESC, ShortestTrip ASC