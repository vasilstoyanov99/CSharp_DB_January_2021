CREATE PROC usp_GetEmployeesFromTown 
		(@TownName VARCHAR(MAX))
AS
BEGIN
	SELECT FirstName AS 'First Name', LastName AS 'Last Name'
	FROM Employees AS emp
	JOIN Addresses AS adr ON emp.AddressID = adr.AddressID
	JOIN Towns AS t ON adr.TownID = t.TownID
	WHERE t.[Name] = @TownName
END

GO 

EXEC usp_GetEmployeesFromTown 'Sofia'