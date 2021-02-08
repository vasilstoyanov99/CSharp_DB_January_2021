--USE ColonialJourney

CREATE TABLE Planets
(
	Id INT PRIMARY KEY IDENTITY, 
	[Name] VARCHAR(30) NOT NULL,
)

CREATE TABLE Spaceports
(
	Id INT PRIMARY KEY IDENTITY, 
	[Name] VARCHAR(50) NOT NULL,
	PlanetId INT REFERENCES Planets(Id)
)

CREATE TABLE Spaceships
(
	Id INT PRIMARY KEY IDENTITY, 
	[Name] VARCHAR(50) NOT NULL,
	Manufacturer VARCHAR(30) NOT NULL,
	LightSpeedRate INT
)

CREATE TABLE Colonists
(
	Id INT PRIMARY KEY IDENTITY, 
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	Ucn VARCHAR(10) NOT NULL UNIQUE,
	BirthDate DATE NOT NULL
)

CREATE TABLE Journeys
(
	Id INT PRIMARY KEY IDENTITY, 
	JourneyStart DATETIME NOT NULL,
	JourneyEnd DATETIME NOT NULL,
	Purpose VARCHAR(11) NULL 
		CHECK (Purpose IN ('Medical', 'Technical', 'Educational', 'Military')),
	DestinationSpaceportId INT REFERENCES Spaceports(Id),
	SpaceshipId INT REFERENCES Spaceships(Id)
)

CREATE TABLE TravelCards
(
	Id INT PRIMARY KEY IDENTITY, 
	CardNumber VARCHAR(10) NOT NULL UNIQUE,
	JobDuringJourney VARCHAR(8) NULL
		CHECK (JobDuringJourney IN ('Pilot', 'Engineer', 'Trooper', 'Cleaner', 'Cook')),
	ColonistId INT REFERENCES Colonists(Id),
	JourneyId INT REFERENCES Journeys(Id)
)