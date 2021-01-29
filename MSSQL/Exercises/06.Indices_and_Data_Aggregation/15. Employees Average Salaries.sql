SELECT * INTO Employees_With_Higher_Salary
FROM Employees
WHERE Salary > 30000 

DELETE FROM Employees_With_Higher_Salary
WHERE ManagerID = 42

UPDATE Employees_With_Higher_Salary
SET Salary += 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS 'AverageSalary'
FROM Employees_With_Higher_Salary
GROUP BY DepartmentID
