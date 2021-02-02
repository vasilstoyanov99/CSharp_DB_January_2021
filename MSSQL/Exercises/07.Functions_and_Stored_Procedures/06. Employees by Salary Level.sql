CREATE PROC usp_EmployeesBySalaryLevel
		(@Level VARCHAR(10))
AS
	BEGIN
		SELECT FirstName AS 'First Name', LastName AS 'Last Name'
		FROM Employees
		WHERE @Level = dbo.ufn_GetSalaryLevel(Salary);
	END
GO 

EXEC usp_EmployeesBySalaryLevel 'high'