SELECT p.PeakName, m.MountainRange AS 'Mountain', c.CountryName, cn.ContinentName
FROM Peaks AS p
	JOIN Mountains AS m ON p.MountainId = m.Id
	JOIN MountainsCountries AS mc ON mc.MountainId = m.Id
	JOIN Countries AS c ON mc.CountryCode = c.CountryCode
	JOIN Continents AS cn ON c.ContinentCode = cn.ContinentCode
ORDER BY p.PeakName ASC, c.CountryName ASC