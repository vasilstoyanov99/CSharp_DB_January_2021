CREATE FUNCTION ufn_GetSalaryLevel (@salary DECIMAL(18,4))
RETURNS VARCHAR(15)
AS
BEGIN	
	DECLARE @Result VARCHAR(15);
		IF @salary < 30000 
			SET @Result = 'Low'
		ELSE IF @salary BETWEEN 30000 AND 50000	
			SET @Result = 'Average'
		ELSE IF @salary > 50000
			SET @Result = 'High'
	RETURN @Result
END

GO

SELECT Salary, dbo.ufn_GetSalaryLevel(Salary) AS 'Salary Level'
FROM Employees