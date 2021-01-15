CREATE TABLE Categories
(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(100) NOT NULL,
	DailyRate INT NOT NULL,
	WeeklyRate INT NOT NULL,
	MonthlyRate INT NOT NULL,
	WeekendRate INT NOT NULL
)

CREATE TABLE Cars
(
	Id INT PRIMARY KEY IDENTITY,
	PlateNumber NVARCHAR(50) NOT NULL,
	Manufacturer NVARCHAR(50) NOT NULL,
	Model NVARCHAR(50) NOT NULL,
	CarYear INT NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Doors INT NOT NULL,
	Picture NVARCHAR(MAX) NOT NULL,
	Condition NVARCHAR(200),
	Available BIT NOT NULL
)

CREATE TABLE Employees
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Title NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE Customers
(
	Id INT PRIMARY KEY IDENTITY,
	DriverLicenceNumber NVARCHAR(50) NOT NULL,
	FullName NVARCHAR(60) NOT NULL,
	[Address] NVARCHAR(200) NOT NULL,
	City NVARCHAR(20) NOT NULL,
	ZIPCode INT NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE RentalOrders 
(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
	CarId INT FOREIGN KEY REFERENCES Cars(Id),
	TankLevel FLOAT NOT NULL,
	KilometrageStart FLOAT NOT NULL,
	KilometrageEnd FLOAT NOT NULL,
	TotalKilometrage INT NOT NULL,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL,
	TotalDays INT NOT NULL,
	RateApplied DECIMAL NOT NULL,
	TaxRate DECIMAL NOT NULL,
	OrderStatus BIT NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate) VALUES
('Nice one', 50, 300, 3000, 1000),
('Nice one', 50, 300, 3000, 1000),
('Nice one', 50, 300, 3000, 1000)

INSERT INTO Cars 
(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available) VALUES
('848561890108BH', 'Tesla', 'Model S', 2019, 1, 4, 'shorturl.at/kpJK3', 'Excellent', 1),
('848561890108BH', 'Tesla', 'Model S', 2019, 1, 4, 'shorturl.at/kpJK3', 'Excellent', 1),
('848561890108BH', 'Tesla', 'Model S', 2019, 1, 4, 'shorturl.at/kpJK3', 'Excellent', 1)

INSERT INTO Employees (FirstName, LastName, Title, Notes) VALUES
('Kevin', 'Hearth', 'Manager', NULL),
('Kevin', 'Hearth', 'Manager', NULL),
('Kevin', 'Hearth', 'Manager', NULL)

INSERT INTO Customers (DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes) VALUES
('480959049TG', 'Harry Potter', 'Hogwarts School of Witchcraft and Wizardry', 'Unknown', 89490, NULL),
('480959049TG', 'Harry Potter', 'Hogwarts School of Witchcraft and Wizardry', 'Unknown', 89490, NULL),
('480959049TG', 'Harry Potter', 'Hogwarts School of Witchcraft and Wizardry', 'Unknown', 89490, NULL)

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd,
TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes) VALUES
(1, 1, 2, 80.5, 100, 1000, 2000, '1/15/2021', '2/15/2021', 30, 25.2, 10, 1, NULL),
(1, 1, 2, 80.5, 100, 1000, 2000, '1/15/2021', '2/15/2021', 30, 25.2, 10, 1, NULL),
(1, 1, 2, 80.5, 100, 1000, 2000, '1/15/2021', '2/15/2021', 30, 25.2, 10, 1, NULL)