SELECT MIN(sub.AVGSalary) AS 'MinAverageSalary'
FROM (
	  SELECT AVG(Salary) AS 'AVGSalary'
	  FROM Employees AS e
	  GROUP BY e.DepartmentID
	  ) AS sub