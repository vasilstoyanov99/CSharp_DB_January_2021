SELECT [Email Provider], COUNT([Email Provider]) AS 'Number Of Users'
FROM (SELECT RIGHT(Email, LEN(Email) - CHARINDEX('@', Email)) AS 'Email Provider'
		FROM Users) AS [emails]
GROUP BY [Email Provider]
ORDER BY [Number Of Users] DESC, [Email Provider] ASC