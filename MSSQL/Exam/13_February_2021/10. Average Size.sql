SELECT u.Username, AVG(f.Size) AS 'Size'
FROM Commits AS c
	JOIN Users AS u ON c.ContributorId = u.Id
	JOIN Files AS f ON f.CommitId = c.Id
GROUP BY u.Username
ORDER BY 'Size' DESC, u.Username ASC