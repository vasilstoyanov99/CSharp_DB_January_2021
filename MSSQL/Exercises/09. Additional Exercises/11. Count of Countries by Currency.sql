SELECT cr.CurrencyCode AS 'CurrencyCode', 
	   cr.[Description] AS 'Currency', 
	   COUNT(c.CountryName) AS 'NumberOfCountries'
FROM Countries AS c
	LEFT JOIN Currencies AS cr ON cr.CurrencyCode = c.CurrencyCode
GROUP BY cr.CurrencyCode, cr.[Description]
ORDER BY 'NumberOfCountries' DESC, cr.[Description] ASC