SELECT cn.ContinentName, 
	   SUM(CONVERT(BIGINT, c.AreaInSqKm)) AS 'CountriesArea', 
	   SUM(CONVERT(BIGINT, c.[Population])) AS 'CountriesPopulation'
FROM Continents AS cn
	JOIN Countries AS c ON c.ContinentCode = cn.ContinentCode
GROUP BY cn.ContinentName
ORDER BY 'CountriesPopulation' DESC