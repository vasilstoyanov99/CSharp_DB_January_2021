CREATE PROC usp_GetHoldersWithBalanceHigherThan 
		(@Money DECIMAL(18, 4))
 AS
	BEGIN
		SELECT FirstName AS 'First Name', 
			 LastName AS 'Last Name' 
			FROM AccountHolders AS ah
				JOIN Accounts AS a ON a.AccountHolderId = ah.Id
			GROUP BY FirstName, LastName
			HAVING SUM(Balance) > @Money
			ORDER BY FirstName, LastName
	END

GO

EXEC usp_GetHoldersWithBalanceHigherThan 500000