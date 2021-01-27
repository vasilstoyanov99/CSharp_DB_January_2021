SELECT TOP(5) e.EmployeeId, e.JobTitle, a.AddressID, a.AddressText
FROM Employees AS e
JOIN Addresses AS a ON e.AddressID = a.AddressID
ORDER BY a.AddressId ASC