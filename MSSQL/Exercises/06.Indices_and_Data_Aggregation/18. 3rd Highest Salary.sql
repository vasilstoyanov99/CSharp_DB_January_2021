SELECT DISTINCT Results.DepartmentID, Results.Salary
FROM 
	(SELECT DepartmentID, 
		Salary, 
		DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) 
		AS 'Ranked'
	FROM Employees) AS [Results]
WHERE Results.Ranked = 3