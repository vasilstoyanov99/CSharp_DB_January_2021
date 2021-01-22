SELECT TownId, [Name]
FROM Towns
WHERE NOT [Name] LIKE '[RBD]%'
ORDER BY [Name] ASC