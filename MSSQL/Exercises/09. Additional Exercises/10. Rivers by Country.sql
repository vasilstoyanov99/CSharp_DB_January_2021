SELECT c.CountryName, 
	   cn.ContinentName, 
	   IIF(COUNT(r.Id) < 1, '0', COUNT(r.Id)) AS 'RiversCount', 
	   IIF(SUM(r.[Length]) IS NULL, '0', SUM(r.[Length])) AS 'TotalLength'
FROM Countries AS c
	LEFT JOIN Continents AS cn ON cn.ContinentCode = c.ContinentCode
	LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
	LEFT JOIN Rivers AS r ON cr.RiverId = r.Id
GROUP BY c.CountryName, cn.ContinentName
ORDER BY 'RiversCount' DESC, TotalLength DESC, c.CountryName ASC