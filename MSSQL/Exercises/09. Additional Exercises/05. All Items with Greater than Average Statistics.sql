SELECT i.[Name], 
	   i.Price, 
	   i.MinLevel AS 'MinLevel',
	   s.Strength, 
	   s.Defence,
	   s.Speed,
	   s.Luck,
	   s.Mind
FROM Items AS i
	JOIN [Statistics] AS s ON i.StatisticId = s.Id
WHERE s.Mind > (SELECT AVG(Mind) FROM [Statistics]) AND 
	  s.Luck > (SELECT AVG(Luck) FROM [Statistics]) AND 
	  s.Speed > (SELECT  AVG(Speed) FROM [Statistics])
ORDER BY i.[Name] ASC