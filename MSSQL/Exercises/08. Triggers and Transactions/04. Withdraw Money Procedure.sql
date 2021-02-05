CREATE PROC usp_WithdrawMoney (@AccountId INT, @MoneyAmount DECIMAL(12,4))
AS
	BEGIN TRANSACTION
		IF(@MoneyAmount < 0)
			THROW 50003, 'Money cannot be negative!', 1
		IF (SELECT COUNT(*) FROM Accounts WHERE Id = @AccountId) < 1
			THROW 50004, 'Clound not find account!', 1
		UPDATE Accounts
		SET Balance -= @MoneyAmount
		WHERE Id = @AccountId
			COMMIT

GO

EXEC usp_WithdrawMoney 1, 100

