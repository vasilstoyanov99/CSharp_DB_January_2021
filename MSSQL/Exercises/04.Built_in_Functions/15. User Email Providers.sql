SELECT [Username], 
RIGHT([Email], LEN([Email]) - PATINDEX('%@%', [Email])) AS 'Email Provider' 
FROM Users
ORDER BY [Email Provider] ASC, [Username] ASC