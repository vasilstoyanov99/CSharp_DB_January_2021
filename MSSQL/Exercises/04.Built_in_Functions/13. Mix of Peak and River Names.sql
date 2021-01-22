SELECT [PeakName], [RiverName],
LOWER(CONCAT(SUBSTRING([PeakName], 1, LEN([PeakName]) - 1), [RiverName])) AS Mix
FROM Peaks AS p, Rivers AS r
WHERE RIGHT(p.[PeakName], 1) = LEFT(r.[RiverName], 1)
ORDER BY Mix