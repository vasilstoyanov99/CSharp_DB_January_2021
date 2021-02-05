CREATE PROC usp_AssignProject(@emloyeeId INT, @projectID INT)
AS
	BEGIN TRANSACTION
		INSERT INTO  EmployeesProjects VALUES
			(@emloyeeId, @projectID)
		IF (SELECT COUNT(*) FROM EmployeesProjects WHERE EmployeeID = @emloyeeId) > 3
			BEGIN
				ROLLBACK;
				THROW 50004, 'The employee has too many projects!', 1
			END
		COMMIT

GO 

EXEC usp_AssignProject 250, 1