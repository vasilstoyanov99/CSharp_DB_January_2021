CREATE TABLE Clients
(
	ClientId INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Phone VARCHAR(12) NOT NULL
)

CREATE TABLE Mechanics
(
	MechanicId INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	[Address] VARCHAR(255) NOT NULL
)

CREATE TABLE Models
(
	ModelId INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) UNIQUE NOT NULL,
)

CREATE TABLE Jobs
(
	JobId INT PRIMARY KEY IDENTITY,
	ModelId INT REFERENCES Models(ModelId),
	[Status] VARCHAR(11) 
		DEFAULT 'Pending' 
			CHECK ([Status] IN ('Pending', 'In Progress', 'Finished')) NOT NULL ,
	ClientId INT REFERENCES Clients(ClientId),
	MechanicId INT REFERENCES Mechanics(MechanicId) NULL,
	IssueDate DATE NOT NULL,
	FinishDate DATE NULL,
)

CREATE TABLE Orders
(
	OrderId INT PRIMARY KEY IDENTITY,
	JobId INT REFERENCES Jobs(JobId),
	IssueDate DATE NULL,
	Delivered BIT DEFAULT 0 NOT NULL
)

CREATE TABLE Vendors
(
	VendorId INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) UNIQUE NOT NULL,
)

CREATE TABLE Parts
(
	PartId INT PRIMARY KEY IDENTITY,
	SerialNumber VARCHAR(50) UNIQUE NOT NULL,
	[Description] VARCHAR(255) NULL,
	Price MONEY NOT NULL CHECK (Price >= 0 AND Price <= 9999.99),
	VendorId INT REFERENCES Vendors(VendorId),
	StockQty INT DEFAULT 0 CHECK (StockQty >= 0)
)

CREATE TABLE OrderParts
(
	OrderId INT REFERENCES Orders(OrderId),
	PartId INT REFERENCES Parts(PartId),
	PRIMARY KEY (OrderId, PartId),
	Quantity INT DEFAULT 1 CHECK (Quantity > 0)
)

CREATE TABLE PartsNeeded
(
	JobId INT REFERENCES Jobs(JobId),
	PartId INT REFERENCES Parts(PartId),
	PRIMARY KEY (JobId, PartId),
	Quantity INT DEFAULT 1 CHECK (Quantity > 0)
)