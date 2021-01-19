CREATE TABLE Persons
(
	PersonID INT NOT NULL,
	FirstName NVARCHAR(60) NOT NULL,
	Salary DECIMAL(7, 2) NOT NULL,
)

CREATE TABLE Passports
(
	PassportID INT PRIMARY KEY IDENTITY(101, 1),
	PassportNumber NVARCHAR(MAX)
)

ALTER TABLE Persons
ADD PassportID INT 

ALTER TABLE Persons 
ADD CONSTRAINT PK_PersonID PRIMARY KEY (PersonID)

ALTER TABLE Persons
ADD FOREIGN KEY (PassportID) REFERENCES Passports(PassportID)

INSERT INTO Passports (PassportNumber) VALUES
('N34FG21B'),
('K65LO4R7'),
('ZE657QP2')

INSERT INTO Persons (PersonID, FirstName, Salary, PassportID) VALUES 
(1, 'Roberto', 43300.00, 102),
(2, 'Tom', 56100.00, 103),
(3, 'Yana', 60200.00, 101)