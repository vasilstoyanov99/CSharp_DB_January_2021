CREATE TABLE Users
(
	Id BIGINT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture NVARCHAR(MAX) NOT NULL,
	LastLoginTime DATETIME,
	IsDeleted BIT
)

INSERT INTO Users (Username, [Password], ProfilePicture, LastLoginTime, IsDeleted) VALUES
('Kevin', 'sdlfhewriog', 'shorturl.at/csGIK', '1/15/2021', 0),
('Kevin', 'sdlfhewriog', 'shorturl.at/csGIK', '1/15/2021', 0),
('Kevin', 'sdlfhewriog', 'shorturl.at/csGIK', '1/15/2021', 0),
('Kevin', 'sdlfhewriog', 'shorturl.at/csGIK', '1/15/2021', 0),
('Kevin', 'sdlfhewriog', 'shorturl.at/csGIK', '1/15/2021', 0)