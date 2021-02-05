CREATE TABLE Logs 
(
	LogId INT PRIMARY KEY IDENTITY,
	AccountId INT NOT NULL REFERENCES Accounts(Id),
	OldSum MONEY NOT NULL,
	NewSum MONEY NOT NULL
)

GO

CREATE TRIGGER tr_OnAccountChangeAddToLogsRecords
ON Accounts FOR UPDATE
AS 
	BEGIN
		INSERT Logs(AccountId, OldSum, NewSum)
		SELECT i.Id, d.Balance, i.Balance
		FROM inserted AS i
			JOIN deleted AS d ON i.Id = d.Id
		WHERE i.Balance != d.Balance
	END