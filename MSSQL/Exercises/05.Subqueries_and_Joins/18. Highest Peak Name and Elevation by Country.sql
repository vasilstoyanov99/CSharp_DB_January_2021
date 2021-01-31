SELECT TOP(5) Result.CountryName, Result.PeakName, Result.HighestPeak, Result.MountainRange
FROM (SELECT CountryName, 
	ISNULL(p.PeakName, '(no highest peak)') AS 'PeakName',
	ISNULL(m.MountainRange, '(no mountain)') AS MountainRange,
	ISNULL(MAX(p.Elevation), 0) AS HighestPeak, 
	DENSE_RANK() OVER(PARTITION BY CountryName ORDER BY MAX(p.Elevation) DESC) AS 'Ranked'
	 FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
	LEFT JOIN Mountains AS m ON mc.MountainId = m.Id
	LEFT JOIN Peaks AS p ON m.Id = P.MountainId
	LEFT JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
	LEFT JOIN Rivers AS r ON cr.RiverId = r.Id
	GROUP BY c.CountryName, p.PeakName, m.MountainRange) AS [Result]
WHERE Ranked = 1
ORDER BY Result.CountryName, Result.PeakName