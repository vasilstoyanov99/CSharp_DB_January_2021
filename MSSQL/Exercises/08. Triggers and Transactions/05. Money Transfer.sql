CREATE PROC usp_TransferMoney (@SenderId INT, @ReceiverId INT, @Amount MONEY)
AS
	BEGIN TRANSACTION
		IF(SELECT Balance FROM Accounts WHERE Id = @SenderId) - @Amount < 0
			THROW 50005, 'Insufficient balance', 1
		EXEC usp_WithdrawMoney @SenderId, @Amount
		EXEC usp_DepositMoney @ReceiverId, @Amount
	COMMIT

GO

EXEC usp_TransferMoney 1, 2, 10