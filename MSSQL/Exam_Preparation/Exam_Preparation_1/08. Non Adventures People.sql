SELECT p.FirstName, p.LastName, p.Age
FROM Passengers AS p
	LEFT JOIN Tickets AS t ON p.Id = t.PassengerId
WHERE t.PassengerId IS NULL
ORDER BY p.Age DESC, P.FirstName ASC, p.LastName ASC