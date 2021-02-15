SELECT * 
FROM (SELECT Id, [Name], CONCAT(Size, 'KB') AS 'Size'
		FROM Files) AS [k]
WHERE k.Id NOT IN (SELECT ParentId FROM Files WHERE ParentId = k.Id)
ORDER BY Id ASC, [Name] ASC, 'Size' DESC