CREATE TABLE People
(
	Id INT PRIMARY KEY IDENTITY, 
	[Name] NVARCHAR(200) NOT NULL, 
	Picture VARCHAR(MAX) NULL, 
	Height DECIMAL (10, 2) NULL,
	[Weight] DECIMAL (10, 2) NULL, 
	Gender VARCHAR(1) NOT NULL,
	Birthdate DATETIME NOT NULL, 
	Biography NVARCHAR(MAX) NULL
)

ALTER TABLE People ADD CONSTRAINT
CK_Gender CHECK (Gender = 'm' OR Gender = 'f')

INSERT INTO People ([Name], Picture, Height, [Weight], Gender, Birthdate, Biography) VALUES
('Kevin', 'shorturl.at/kpCDJ', 420.20, 69.69, 'm', 4/20/1969, 'I love memes'),
('Kevin', 'shorturl.at/kpCDJ', 420.20, 69.69, 'm', 4/20/1969, 'I love memes'),
('Kevin', 'shorturl.at/kpCDJ', 420.20, 69.69, 'm', 4/20/1969, 'I love memes'),
('Kevin', 'shorturl.at/kpCDJ', 420.20, 69.69, 'm', 4/20/1969, 'I love memes'),
('Kevin', 'shorturl.at/kpCDJ', 420.20, 69.69, 'm', 4/20/1969, 'I love memes')
