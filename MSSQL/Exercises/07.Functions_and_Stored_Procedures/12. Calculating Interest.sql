CREATE PROC usp_CalculateFutureValueForAccount 
		(@accountId INT, @interestRate FLOAT)
AS
	BEGIN
		SELECT a.Id,
			ah.FirstName,
			ah.LastName,
			a.Balance,
			dbo.ufn_CalculateFutureValue(a.Balance, @interestRate, 5)
		FROM AccountHolders AS ah
		JOIN Accounts AS a ON a.AccountHolderId = ah.Id
		WHERE a.Id = @accountId
	END

GO

EXEC usp_CalculateFutureValueForAccount 1, 0.1