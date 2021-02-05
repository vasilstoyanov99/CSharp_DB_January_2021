CREATE PROC usp_DepositMoney (@AccountId INT, @MoneyAmount DECIMAL(12, 4))
AS
	BEGIN TRANSACTION
		IF(@MoneyAmount < 0)
			THROW 50001, 'Cannot add negative money!', 1
		IF(SELECT COUNT(*) FROM Accounts WHERE Id = @AccountId) < 1
			THROW 50002, 'Clound not find account!', 1
		UPDATE Accounts
		SET Balance += @MoneyAmount
		WHERE Id = @AccountId
			COMMIT

GO

EXEC usp_DepositMoney 1, 100