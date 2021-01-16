CREATE TABLE Employees 
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	Title NVARCHAR(60) NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE Customers 
(
	Id INT PRIMARY KEY IDENTITY, 
	AccountNumber INT NOT NULL,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	PhoneNumber INT NOT NULL,
	EmergencyName NVARCHAR(60) NOT NULL,
	EmergencyNumber INT NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE RoomStatus
(
	Id INT PRIMARY KEY IDENTITY,
	RoomStatus NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE RoomTypes 
(
	Id INT PRIMARY KEY IDENTITY,
	RoomType NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE BedTypes 
(
	Id INT PRIMARY KEY IDENTITY,
	BedType NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE Rooms 
(
	Id INT PRIMARY KEY IDENTITY,
	RoomNumber INT NOT NULL,
	RoomType INT FOREIGN KEY REFERENCES RoomTypes(Id),
	BedType INT FOREIGN KEY REFERENCES BedTypes(Id),
	Rate FLOAT NOT NULL,
	RoomStatus INT FOREIGN KEY REFERENCES RoomStatus(Id),
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE Payments 
(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	PaymentDate DATETIME NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(Id),
	FirstDateOccupied DATETIME NOT NULL,
	LastDateOccupied DATETIME NOT NULL,
	TotalDays INT NOT NULL,
	AmountCharged DECIMAL NOT NULL,
	TaxRate DECIMAL NOT NULL,
	TaxAmount DECIMAL NOT NULL,
	PaymentTotal DECIMAL NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

CREATE TABLE Occupancies 
(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	DateOccupied DATETIME NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(Id),
	RoomNumber INT FOREIGN KEY REFERENCES Rooms(Id),
	RateApplied DECIMAL NOT NULL,
	PhoneCharge DECIMAL NOT NULL,
	Notes NVARCHAR(MAX) NULL
)

INSERT INTO Employees (FirstName, LastName, Title, Notes) VALUES
('G', 'Eazy', 'Manager', NULL),
('G', 'Eazy', 'Manager', NULL),
('G', 'Eazy', 'Manager', NULL)

INSERT INTO Customers 
(AccountNumber, FirstName, LastName, PhoneNumber,
EmergencyName, EmergencyNumber, Notes) VALUES
(1, 'Post', 'Malone', 89401490, 'PS',65106015, NULL),
(1, 'Post', 'Malone', 89401490, 'PS',65106015, NULL),
(1, 'Post', 'Malone', 89401490, 'PS',65106015, NULL)

INSERT INTO RoomStatus (RoomStatus, Notes) VALUES
('Nice', NULL),
('Nice', NULL),
('Nice', NULL)

INSERT INTO RoomTypes (RoomType, Notes) VALUES
('The best', NULL),
('The best', NULL),
('The best', NULL)

INSERT INTO BedTypes (BedType, Notes) VALUES
('King size', NULL),
('King size', NULL),
('King size', NULL)

INSERT INTO Rooms 
(RoomNumber, RoomType, BedType, Rate, RoomStatus, Notes) VALUES
(5, 1, 2, 8.9, 1, NULL),
(5, 1, 2, 8.9, 1, NULL),
(5, 1, 2, 8.9, 1, NULL)

INSERT INTO Payments
(EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, 
LastDateOccupied, TotalDays, AmountCharged, TaxRate, 
TaxAmount, PaymentTotal, Notes) VALUES
(1, '01/01/2021', 2, '01/01/2021', '08/12/2020', 20, 10000, 20.5, 30.8, 15000, NULL),
(2, '01/01/2021', 1, '01/01/2021', '08/12/2020', 20, 10000, 20.5, 30.8, 15000, NULL),
(3, '01/01/2021', 3, '01/01/2021', '08/12/2020', 20, 10000, 20.5, 30.8, 15000, NULL)

INSERT INTO Occupancies
(EmployeeId, DateOccupied, AccountNumber, RoomNumber,
RateApplied, PhoneCharge, Notes) VALUES
(1, '01/01/2021', 2, 1, 30.8, 50, NULL),
(1, '01/01/2021', 2, 1, 30.8, 50, NULL),
(1, '01/01/2021', 2, 1, 30.8, 50, NULL)