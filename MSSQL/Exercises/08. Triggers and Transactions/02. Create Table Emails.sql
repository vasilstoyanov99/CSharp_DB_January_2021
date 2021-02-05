CREATE TABLE NotificationEmails
(
	Id INT PRIMARY KEY IDENTITY,
	Recipient INT REFERENCES Accounts(Id),
	[Subject] NVARCHAR(MAX) NOT NULL,
	Body NVARCHAR(MAX) NOT NULL
)

GO

CREATE TRIGGER tr_OnNewRecordAddCreateNewNotificationEmail
ON Logs FOR INSERT
AS
	BEGIN
		INSERT NotificationEmails(Recipient, [Subject], Body)
		SELECT i.AccountID AS 'Recipient',
		'Balance change for account: ' + CAST(i.AccountID AS NVARCHAR(20)) AS 'Subject',
		'On ' 
		+ CONVERT(NVARCHAR(25), GETDATE(), 100) 
		+ ' your balance was changed from ' + CAST (i.OldSum AS NVARCHAR(25))
		+ ' to ' + CAST(i.NewSum AS NVARCHAR(25)) + '.' AS 'Body'
		FROM inserted AS i
	END