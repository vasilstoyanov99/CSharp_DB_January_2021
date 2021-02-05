CREATE TABLE Deleted_Employees
(
	EmployeeId  INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	MiddleName VARCHAR(50) NOT NULL,
	JobTitle VARCHAR(50) NOT NULL,
	DepartmentId INT NOT NULL,
	Salary MONEY NOT NULL
)

GO

CREATE TRIGGER Add_Employee_Records_When_It_Is_Deleted
ON Employees AFTER DELETE
AS
	BEGIN
		INSERT INTO Deleted_Employees
			(FirstName, LastName, MiddleName, JobTitle, DepartmentId, Salary)
		SELECT d.FirstName, d.LastName, 
			   d.MiddleName, d.JobTitle, d.DepartmentID, d.Salary
		FROM deleted AS d
	END