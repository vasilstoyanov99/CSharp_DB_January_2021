CREATE TABLE Directors
(
	Id INT PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR(200) NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE Genres 
(
	Id INT PRIMARY KEY IDENTITY, 
	GenreName NVARCHAR(100) NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE Categories 
(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(100) NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE Movies
(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(200) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id),
	CopyrightYear DATETIME2 NOT NULL,
	[Length] TIME NOT NULL, 
	GenreId INT FOREIGN KEY REFERENCES Genres(Id),
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Rating DECIMAL(2, 1) NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

INSERT INTO Directors (DirectorName, Notes) VALUES
('Martin Scorsese', 'Genius'),
('Martin Scorsese', 'Genius'),
('Martin Scorsese', NULL),
('Martin Scorsese', 'Genius'),
('Martin Scorsese', 'Genius')

INSERT INTO Genres (GenreName, Notes) VALUES
('gangster movies', 'I keep it gangsta'), 
('action', 'and why should I change that'),
('horror movies', 'Fk you all you motherfkers tryin'' to change rap'),
('comedy movies', 'If I rob you of knowledge ain''t nothin'' to it'),
('science fiction', 'Gangsta rap made me do it')

INSERT INTO Categories (CategoryName, Notes) VALUES
('- You can get my OF for only 30 bucks', NULL),
('Silence wench', NULL),
('I do not wish to be horny any more', NULL),
('I just want to be happy', NULL),
('Noice', NULL)


INSERT INTO Movies (Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes) VALUES
('Noice', 1, '2001', '2:30', 1, 5, 8.9, NULL),
('Noice', 1, '2001', '2:30', 2, 4, 8.9, NULL),
('Noice', 1, '2001', '2:30', 3, 3, 8.9, NULL),
('Noice', 1, '2001', '2:30', 4, 2, 8.9, NULL),
('Noice', 1, '2001', '2:30', 5, 1, 8.9, NULL)

